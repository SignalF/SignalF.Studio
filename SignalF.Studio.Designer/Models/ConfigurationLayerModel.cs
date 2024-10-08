using Scotec.Blazor.Diagrams.Core.Geometry;
using Scotec.Blazor.Diagrams.Core.Layer;
using SignalF.Datamodel.Configuration;
using SignalF.Datamodel.Signals;

namespace SignalF.Studio.Designer.Models;

public class ConfigurationLayerModel : NodeLayerModel<SignalProcessorNodeModel, SignalProcessorLinkModel>
{
    private readonly DocumentManager _documentManager;

    public ConfigurationLayerModel(DocumentManager documentManager)
    {
        _documentManager = documentManager;
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
        var x = 100;
        var y = 100;

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
        var node = new SignalProcessorNodeModel(configuration, position, size)
        {
            Name = configuration.Name
        };

        return node;
    }

    public SignalProcessorNodeModel CreateSignalProcessorNode(DefinitionNodeModel definition, Point position)
    {
        var signalProcessorConfiguration = definition.CreateItem(position);
        var node = CreateSignalProcessorNode(signalProcessorConfiguration, position, new Size(240, 300));

        return node;
    }
}
