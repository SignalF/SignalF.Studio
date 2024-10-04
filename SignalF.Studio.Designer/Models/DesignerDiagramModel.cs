using Scotec.Blazor.Diagrams;
using Scotec.Blazor.Diagrams.Core.Geometry;
using Scotec.Blazor.Diagrams.Core.Layer;
using Scotec.Blazor.Diagrams.Core.Models;
using Scotec.XMLDatabase;
using SignalF.Datamodel.Configuration;
using SignalF.Datamodel.Signals;

namespace SignalF.Studio.Designer.Models;

public class DesignerDiagramModel : BlazorDiagramModel
{
    private readonly DocumentManager _documentManager;
    private readonly List<SignalProcessorNodeModel> _nodeModels = [];

    public DesignerDiagramModel(DocumentManager documentManager, IEnumerable<LayerModel> layers)
    {
        _documentManager = documentManager;
        
        AddLayers(layers);
    }

    public IReadOnlyList<SignalProcessorNodeModel> NodeModels => _nodeModels;

    public override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        var configuration = _documentManager.GetConfiguration();

        //var options = new BlazorDiagramOptions
        //{
        //    AllowMultiSelection = true,
        //    Zoom =
        //    {
        //        Enabled = true
        //    },
        //    Links =
        //    {
        //        DefaultRouter = new OrthogonalRouter(),
        //        DefaultPathGenerator = new StraightPathGenerator(),
        //        EnableSnapping = true,
        //        //SnappingRadius = 40, 
        //        RequireTarget = true
        //    },

        //    LinksLayerOrder = 1
        //};

        //CreateSignalProcessorNodes(configuration);
        //CreateConnections(configuration);

        //Diagram.Links.Added += OnLinkAdded;
        //Diagram.Links.Removed += OnLinkRemoved;

        configuration.Session.DataChanged += SessionOnDataChanged;
    }

    //private void OnLinkAdded(BaseLinkModel link)
    //{
    //    //link.Segmentable = true;
    //    link.Router = new OrthogonalRouter();
    //    link.PathGenerator = new StraightPathGenerator();
    //    link.TargetAttached += LinkOnTargetAttached;
    //}

    //private void OnLinkRemoved(BaseLinkModel link)
    //{
    //    link.TargetAttached -= LinkOnTargetAttached;

    //    var linkId = new Guid(link.Id);
    //    var configuration = _documentManager.GetConfiguration();
    //    var connection = configuration.Connections.FirstOrDefault(item => item.Id == linkId);

    //    if (connection != null)
    //    {
    //        //TODO: Execute in transaction.
    //        configuration.Connections.Delete(connection);
    //    }
    //}

    //private void LinkOnTargetAttached(BaseLinkModel link)
    //{
    //    link.TargetAttached -= LinkOnTargetAttached;

    //    var source = (SignalProcessorPortModel)link.Source.Model;
    //    var target = (SignalProcessorPortModel)link.Target.Model;

    //    var configuration = _documentManager.GetConfiguration();
    //    var connection = configuration.Connections.Create();

    //    connection.SignalSource = (ISignalSourceConfiguration)(source.SignalConfiguration is ISignalSourceConfiguration
    //        ? source.SignalConfiguration
    //        : target.SignalConfiguration);

    //    connection.SignalSink = (ISignalSinkConfiguration)(target.SignalConfiguration is ISignalSinkConfiguration
    //        ? target.SignalConfiguration
    //        : source.SignalConfiguration);

    //    Diagram.Links.Remove(link);

    //    link = CreateLink(connection, source, target);
    //    Diagram.Links.Add(link);

    //}

        //private void CreateConnections(IControllerConfiguration configuration)
        //{
        //    var links = new List<LinkModel>();
        //    foreach (var connection in configuration.Connections)
        //    {
        //        var signalSink = connection.SignalSink;
        //        var signalSource = connection.SignalSource;
        //        var signalSinkPort = FindPort(signalSink);
        //        var signalSourcePort = FindPort(signalSource);

        //        var link = CreateLink(connection, signalSourcePort, signalSinkPort);
        //        links.Add(link);
        //    }

        //    Diagram.Links.Add(links);
        //}

        //private static LinkModel CreateLink(ISignalConnection connection, SignalProcessorPortModel signalSourcePort, SignalProcessorPortModel signalSinkPort)
        //{
        //    var link = new LinkModel(connection.Id.ToString("D"),signalSinkPort, signalSourcePort)
        //    {
        //        Router = new OrthogonalRouter(),
        //        PathGenerator = new StraightPathGenerator()
        //    };
        //    return link;
        //}

        //private SignalProcessorPortModel FindPort(ISignalConfiguration signalConfiguration)
        //{
        //    return NodeModels
        //           .SelectMany(node => node.Ports)
        //           .Cast<SignalProcessorPortModel>()
        //           .First(node => node.SignalConfiguration == signalConfiguration);
        //}

        private void SessionOnDataChanged(object sender, DataChangedEventArgs e)
    {
        foreach (var signalProcessorConfiguration in e.GetChanges<ISignalProcessorConfiguration>(EChangeType.Added))
        {
            // _ = Diagram.Nodes.Add(CreateSignalProcessorNode(signalProcessorConfiguration, new Point(x, y), new Size(240, 300)));
        }
    }

}
