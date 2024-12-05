using System.Collections.Immutable;
using Scotec.Blazor.Diagrams.Core.Behaviours;
using Scotec.Blazor.Diagrams.Core.Layer;
using Scotec.Blazor.Diagrams.Core.Models;
using Scotec.XMLDatabase;
using Scotec.XMLDatabase.ChangeNotification;
using SignalF.Datamodel.Designer;
using SignalF.Datamodel.Signals;
using ILinkElement = SignalF.Datamodel.Designer.ILinkElement;

namespace SignalF.Studio.Designer.Models;

public class ConfigurationLayerModel : NodeLayerModel
{
    private readonly DataContext _dataContext;
    private readonly SignalProcessorLinkModel.Factory _linkModelFactory;
    private readonly SignalProcessorNodeModel.Factory _nodeModelFactory;

    public ConfigurationLayerModel(DiagramModel diagramModel, 
                                   Func<NodeLayerModel, IEnumerable<INodeLayerBehaviour>> behaviours,
                                   DataContext dataContext, 
                                   SignalProcessorNodeModel.Factory nodeModelFactory,
                                   SignalProcessorLinkModel.Factory linkModelFactory,
                                   INodeLayerBehaviour.Factory testFactory)
        : base(diagramModel, behaviours, testFactory)
    {
        _dataContext = dataContext;
        _nodeModelFactory = nodeModelFactory;
        _linkModelFactory = linkModelFactory;
    }

    public override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        var configuration = _dataContext.GetConfiguration();

        var nodeElements = configuration.DesignerConfiguration.Elements.OfType<ISignalProcessorElement>();
        CreateSignalProcessorNodes(nodeElements);

        var ports = GetAllPorts();

        var linkElements = configuration.DesignerConfiguration.Elements.OfType<ILinkElement>();
        CreateLinks(linkElements, ports);

        _dataContext.Changed += DataContextOnChanged;
    }

    private ImmutableList<SignalProcessorPortModel> GetAllPorts()
    {
        var ports = GetNodes().OfType<SignalProcessorNodeModel>()
                              .SelectMany(node => node.GetPorts<SignalProcessorPortModel>())
                              .ToImmutableList();
        return ports;
    }

    private void DataContextOnChanged(object sender, DataChangedEventArgs args)
    {
        var newSignalProcessorElements = args.GetChanges<ISignalProcessorElement>()
                              .Where(change => change.ChangeType == EChangeNotificationType.Added)
                              .Select(newElement => (ISignalProcessorElement)newElement.BusinessObject);
        CreateSignalProcessorNodes(newSignalProcessorElements);

        var newLinkElements = args.GetChanges<ILinkElement>()
                              .Where(change => change.ChangeType == EChangeNotificationType.Added)
                              .Select(newElement => (ILinkElement)newElement.BusinessObject);
        CreateLinks(newLinkElements, GetAllPorts());
    }

    private void DataContextOnOpened(object sender, EventArgs e)
    {
        var configuration = _dataContext.GetConfiguration();

        var elements = configuration.DesignerConfiguration.Elements.OfType<ISignalProcessorElement>();
        CreateSignalProcessorNodes(elements);
    }

    private void DataContextOnClosed(object sender, EventArgs e)
    {
    }

    private void CreateSignalProcessorNodes(IEnumerable<ISignalProcessorElement> elements)
    {
        AddNodes(elements.Select(element =>
        {
            var node = CreateSignalProcessorNode(element);

            return node;
        }));
    }

    private SignalProcessorNodeModel CreateSignalProcessorNode(ISignalProcessorElement designerElement)
    {
        var node = _nodeModelFactory(designerElement);

        return node;
    }

    private void CreateLinks(IEnumerable<ILinkElement> elements, IReadOnlyList<SignalProcessorPortModel> ports)
    {
        AddLinks(elements.Select(element =>
        {
            var sourcePort = ports.First(port => port.SignalConfiguration == element.Connection.SignalSource);
            var targetPort = ports.First(port => port.SignalConfiguration == element.Connection.SignalSink);

            var link = CreateLink(element, sourcePort, targetPort);

            return link;
        }));
    }

    private LinkModel CreateLink(ILinkElement linkElement, SignalProcessorPortModel sourcePort, SignalProcessorPortModel targetPort)
    {
        var connection = linkElement.Connection;

        var link = _linkModelFactory(linkElement, sourcePort.Anchor, targetPort.Anchor);
        sourcePort.AddLink(link);
        targetPort.AddLink(link);
        return link;
    }

    public override LinkModel CreateDraftLink(AnchorModel source, AnchorModel target)
    {
        var link = new SignalProcessorLinkModel(source, target);

        return link;
    }

    public override void CreateLink(PortModel sourcePort, PortModel targetPort)
    {
        if (sourcePort is not SignalProcessorPortModel source || targetPort is not SignalProcessorPortModel target)
        {
            throw new InvalidCastException();
        }

        var session = _dataContext.GetConfiguration().Session;
        using var changeLock = session.CreateNotificationLock();
        using var transaction = session.CreateTransaction();

        var configuration = _dataContext.GetConfiguration();
        var connection = configuration.Connections.Create();
        connection.SignalSource = (ISignalSourceConfiguration)source.SignalConfiguration;
        connection.SignalSink = (ISignalSinkConfiguration)target.SignalConfiguration;

        var linkElement = configuration.DesignerConfiguration.Elements.Create<ILinkElement>();
        linkElement.Connection = connection;

        transaction.Commit();
    }
}
