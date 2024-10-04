using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scotec.Blazor.Diagrams.Core.Geometry;
using Scotec.Blazor.Diagrams.Core.Layer;
using Scotec.Blazor.Diagrams.Core.Models;
using SignalF.Datamodel.Configuration;
using SignalF.Datamodel.Signals;
using SignalF.Studio.Designer.Widgets;

namespace SignalF.Studio.Designer.Models
{
    public class ConfigurationLayerModel : NodeLayerModel<SignalProcessorNodeModel, SignalProcessorLinkModel>
    {
        private readonly DocumentManager _documentManager;
        private List<SignalProcessorNodeModel> _nodes;

        public ConfigurationLayerModel(DocumentManager documentManager)
        {
            _documentManager = documentManager;
        }

        public IReadOnlyList<SignalProcessorNodeModel> Nodes => _nodes;
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
            var x = 100;
            var y = 100;

            _nodes = configuration.SignalProcessorConfigurations
                                  .Select(signalProcessor =>
                                  {
                                      var node = CreateSignalProcessorNode(signalProcessor, new Point(x, y), new Size(240, 300));

                                      x += 300;
                                      y += 100;

                                      return node;
                                  }).ToList();
        }

        private SignalProcessorNodeModel CreateSignalProcessorNode(ISignalProcessorConfiguration configuration, Point position, Size size)
        {
            var node = new SignalProcessorNodeModel(configuration, position, size)
            {
                Name = configuration.Name
            };

            return node;
        }

        public NodeModel CreateSignalProcessorNode(DefinitionNodeModel definition, Point position)
        {
            var signalProcessorConfiguration = definition.CreateItem(position);
            var node = CreateSignalProcessorNode(signalProcessorConfiguration, position, new Size(240, 300));
            //NodeModels.Add(node);

            return node;
        }

    }
}
