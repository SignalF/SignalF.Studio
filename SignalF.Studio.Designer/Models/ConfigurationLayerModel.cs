#define DEV_CODE
using System.Collections.Immutable;
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
    private readonly Func<ILinkElement, AnchorModel, AnchorModel, SignalProcessorLinkModel> _linkModelFactory;

    public ConfigurationLayerModel(Func<LayerModel, IEnumerable<INodeLayerBehaviour>> behaviours, DataContext dataContext,
                                   Func<ISignalProcessorElement, SignalProcessorNodeModel> nodeModelFactory,
                                   Func<ILinkElement, AnchorModel, AnchorModel, SignalProcessorLinkModel> linkModelFactory)
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

        var ports = GetNodes().OfType<SignalProcessorNodeModel>()
                              .SelectMany(node => node.GetPorts<SignalProcessorPortModel>())
                              .ToImmutableList();

#if DEV_CODE
        var allPorts = GetNodes().OfType<SignalProcessorNodeModel>().SelectMany(node => node.GetPorts<SignalProcessorPortModel>().Select(port => new{Port = port, Node = node})).ToList();

        //TODO: Remove the deletion and creation of the link elements. This is just useful during the implementation phase while it is not possible to creates links in the UI. 

        var links = configuration.DesignerConfiguration.Elements.OfType<ILinkElement>().ToList();
        links.ForEach(link => configuration.DesignerConfiguration.Elements.Delete(link));
        
        foreach (var connection in configuration.Connections)
        {
            var linkElement = configuration.DesignerConfiguration.Elements.Create<ILinkElement>();
            linkElement.Connection = connection;
            var sourcePort = allPorts.First(port => port.Port.SignalConfiguration == connection.SignalSource);
            var sinkPort = allPorts.First(port => port.Port.SignalConfiguration == connection.SignalSink);

            var first = linkElement.Vertices.Create();
            first.X = sourcePort.Port.Anchor.AnchorPoint.X;
            first.Y = sourcePort.Port.Anchor.AnchorPoint.Y;

            var last = linkElement.Vertices.Create();
            last.X = sinkPort.Port.Anchor.AnchorPoint.X;
            last.Y = sinkPort.Port.Anchor.AnchorPoint.Y;
        }
#endif
        var linkElements = configuration.DesignerConfiguration.Elements.OfType<ILinkElement>();
        CreateLinks(linkElements, ports);

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

        //var first = linkElement.Vertices.Create();
        //first.X = sourcePort.Anchor.AnchorPoint.X;
        //first.Y = sourcePort.Anchor.AnchorPoint.Y;

        //var last = linkElement.Vertices.Create();
        //last.X = targetPort.Anchor.AnchorPoint.X;
        //last.Y = targetPort.Anchor.AnchorPoint.Y;
        
        //var node = _linkModelFactory(linkElement, sourcePort.Anchor, targetPort.Anchor);
        var link = new SignalProcessorLinkModel(linkElement, sourcePort.Anchor, targetPort.Anchor);
        sourcePort.AddLink(link);
        targetPort.AddLink(link);
        return link;
    }
}
