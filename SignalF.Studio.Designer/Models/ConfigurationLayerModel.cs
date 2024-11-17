using Scotec.Blazor.Diagrams.Core.Behaviours;
using Scotec.Blazor.Diagrams.Core.Geometry;
using Scotec.Blazor.Diagrams.Core.Layer;
using Scotec.XMLDatabase;
using Scotec.XMLDatabase.ChangeNotification;
using SignalF.Datamodel.Designer;
using System.Xml.Linq;
using Scotec.Blazor.Diagrams.Core.Models;
using ILinkElement = SignalF.Datamodel.Designer.ILinkElement;
using NuGet.Protocol.Plugins;
using Point = Scotec.Blazor.Diagrams.Core.Geometry.Point;

namespace SignalF.Studio.Designer.Models;

public class ConfigurationLayerModel : NodeLayerModel<SignalProcessorNodeModel, SignalProcessorLinkModel>
{
    private readonly DataContext _dataContext;
    private readonly Func<ISignalProcessorElement, SignalProcessorNodeModel> _nodeModelFactory;
    private readonly Func<ILinkElement, SignalProcessorLinkModel> _linkModelFactory;

    public ConfigurationLayerModel(Func<LayerModel, IEnumerable<INodeLayerBehaviour>> behaviours, DataContext dataContext,
                                   Func<ISignalProcessorElement, SignalProcessorNodeModel> nodeModelFactory,
                                   Func<ILinkElement, SignalProcessorLinkModel> linkModelFactory)
        : base(behaviours)
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


        var allPorts = GetNodes().OfType<SignalProcessorNodeModel>().SelectMany(node => node.GetPorts<SignalProcessorPortModel>().Select(port => new{Port = port, Node = node})).ToList();

        var links = configuration.DesignerConfiguration.Elements.OfType<ILinkElement>().ToList();
        links.ForEach(link => configuration.DesignerConfiguration.Elements.Delete(link));
        foreach (var connection in configuration.Connections)
        {
            var linkElement = configuration.DesignerConfiguration.Elements.Create<ILinkElement>();
            linkElement.Connection = connection;
            var sourcePort = allPorts.First(port => port.Port.SignalConfiguration == connection.SignalSource);
            var sinkPort = allPorts.First(port => port.Port.SignalConfiguration == connection.SignalSink);

            var first = linkElement.Vertices.Create();
            first.X = sourcePort.Port.GetAnchorPoint().X + sourcePort.Node.Position.X;
            first.Y = sourcePort.Port.GetAnchorPoint().Y + sourcePort.Node.Position.Y + 40.0;

            var last = linkElement.Vertices.Create();
            last.X = sinkPort.Port.GetAnchorPoint().X + sinkPort.Node.Position.X;
            last.Y = sinkPort.Port.GetAnchorPoint().Y + sinkPort.Node.Position.Y + 40.0;
        }

        var linkElements = configuration.DesignerConfiguration.Elements.OfType<ILinkElement>();
        CreateLinks(linkElements);

        _dataContext.Changed += DataContextOnChanged;
    }

    private void DataContextOnChanged(object sender, DataChangedEventArgs args)
    {
        var newElements = args.GetChanges<ISignalProcessorElement>()
                              .Where(change => change.ChangeType == EChangeNotificationType.Added)
                              .Select(newElement => (ISignalProcessorElement)newElement.BusinessObject);
        CreateSignalProcessorNodes(newElements);

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
    private void CreateLinks(IEnumerable<ILinkElement> elements)
    {
        AddLinks(elements.Select(element =>
        {
            var node = CreateLink(element);

            return node;
        }));
    }

    private LinkModel CreateLink(ILinkElement designerElement)
    {
        var node = _linkModelFactory(designerElement);

        return node;
    }
}
