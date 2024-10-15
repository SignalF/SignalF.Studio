using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Scotec.Blazor.Diagrams.Core.Behaviours;
using Scotec.Blazor.Diagrams.Core.Geometry;
using Scotec.Blazor.Diagrams.Core.Layer;
using SignalF.Datamodel.Configuration;
using SignalF.Datamodel.Signals;

namespace SignalF.Studio.Designer.Models;

public class ConfigurationLayerModel : NodeLayerModel<SignalProcessorNodeModel, SignalProcessorLinkModel>
{
    private readonly DocumentManager _documentManager;
    private readonly Func<ISignalProcessorConfiguration, Point, Size, SignalProcessorNodeModel> _nodeModelFactory;

    public ConfigurationLayerModel(Func<LayerModel, IEnumerable<INodeLayerBehaviour>> behaviours, DocumentManager documentManager, Func<ISignalProcessorConfiguration, Point, Size, SignalProcessorNodeModel> nodeModelFactory)
    : base(behaviours)
    {
        _documentManager = documentManager;
        _nodeModelFactory = nodeModelFactory;
    }

    //public IReadOnlyList<SignalProcessorNodeModel> Nodes => _nodes;
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

        CreateSignalProcessorNodes(configuration);
        //CreateConnections(configuration);

        //Diagram.Links.Added += OnLinkAdded;
        //Diagram.Links.Removed += OnLinkRemoved;

        //configuration.Session.DataChanged += SessionOnDataChanged;
    }

    private void CreateSignalProcessorNodes(IControllerConfiguration configuration)
    {
        //TODO: Get position from configuration.
        var x = 10;
        var y = 10;

        AddNodes(configuration.SignalProcessorConfigurations
                              .Select(signalProcessor =>
                              {
                                  //TODO: Get size from configuration.
                                  var node = CreateSignalProcessorNode(signalProcessor, new Point(x, y), new Size(240, 300));

                                  x += 300;
                                  y += 100;

                                  return node;
                              }));
    }

    private SignalProcessorNodeModel CreateSignalProcessorNode(ISignalProcessorConfiguration configuration, Point position, Size size)
    {
        var node = _nodeModelFactory(configuration, position, size);
        node.Name = configuration.Name;

        return node;
    }

    public SignalProcessorNodeModel CreateSignalProcessorNode(DefinitionNodeModel definition, Point position)
    {
        var signalProcessorConfiguration = definition.CreateItem(position);
        var node = CreateSignalProcessorNode(signalProcessorConfiguration, position, new Size(240, 300));

        return node;
    }
}
