using Blazor.Diagrams;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.PathGenerators;
using Blazor.Diagrams.Core.Routers;
using Blazor.Diagrams.Options;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Scotec.Blazor.DragDrop.Components;
using Scotec.XMLDatabase;
using SignalF.Datamodel.Configuration;
using SignalF.Datamodel.Hardware;
using SignalF.Datamodel.Signals;
using SignalF.Studio.Designer.Model;
using SignalF.Studio.Designer.Model;
using SignalF.Studio.Designer.Widgets;

namespace SignalF.Studio.Designer.Components;

public partial class SfDiagram
{
    private IList<DefinitionNodeModel> _nodeTypes = new List<DefinitionNodeModel>();

    public BlazorDiagram Diagram { get; set; } = null!;

    [Inject] protected DocumentManager? DocumentManager { get; set; }

    //    protected override void OnInitialized()
    protected override Task OnInitializedAsync()
    {
#if !RADZEN
        if (DocumentManager is null)
        {
            return Task.CompletedTask;
        }

        var configuration = DocumentManager.GetConfiguration();

        var options = new BlazorDiagramOptions
        {
            AllowMultiSelection = true,
            Zoom =
            {
                Enabled = true
            },
            Links =
            {
                DefaultRouter = new NormalRouter(),
                DefaultPathGenerator = new SmoothPathGenerator(),
                EnableSnapping = true,
                //SnappingRadius = 40, 
                RequireTarget = true
            }, 
            
            LinksLayerOrder = 1,
            
        };
        Diagram = new BlazorDiagram(options);
        Diagram.RegisterComponent<SignalProcessorNodeModel, SignalProcessor>();
        FillDiagram(configuration);

        Diagram.Links.Added += (link) =>
        {
            //link.Segmentable = true;
            link.Router = new OrthogonalRouter();
            link.PathGenerator = new StraightPathGenerator();
        };

#else
        Diagram = new BlazorDiagram();
#endif

        return Task.CompletedTask;
    }

    private void FillDiagram(IControllerConfiguration configuration)
    {
        var x = 100;
        var y = 100;

        foreach (var signalProcessor in configuration.SignalProcessorConfigurations)
        {
            _ = Diagram.Nodes.Add(CreateSignalProcessorNode(signalProcessor, new Point(x, y), new Size(240, 300)));

            x += 100;
            y += 100;
        }

        configuration.Session.DataChanged += SessionOnDataChanged;

        //var leftPort1 = firstNode.AddPort(new SignalProcessorPort(firstNode, PortAlignment.Left ){});
        //var leftPort1_1 = firstNode.AddPort(new SignalProcessorPort(firstNode, PortAlignment.Left ){});
        //var leftPort1_2 = firstNode.AddPort(new SignalProcessorPort(firstNode, PortAlignment.Left ){});
        //var rightPort1 = firstNode.AddPort(new SignalProcessorPort(firstNode, PortAlignment.Right));

        //var leftPort2 = secondNode.AddPort(new SignalProcessorPort(secondNode, PortAlignment.Left));
        //var rightPort2 = secondNode.AddPort(new SignalProcessorPort(secondNode, PortAlignment.Right));

        //var leftPort = secondNode.AddPort(new SignalProcessorPort());
        //var rightPort = secondNode.AddPort(PortAlignment.Right);
    }

    private SignalProcessorNodeModel CreateSignalProcessorNode(ISignalProcessorConfiguration configuration, Point position, Size size)
    {
        var node = new SignalProcessorNodeModel(configuration, position, size)
        {
            Name = configuration.Name
        };

        //foreach (var signalSink in configuration.SignalSinks)
        //{
        //    var port = node.AddPort(new SignalProcessorPortModel(signalSink, node, PortAlignment.Left ){});
        //}

        //foreach (var signalSource in configuration.SignalSources)
        //{
        //    var port = node.AddPort(new SignalProcessorPortModel(signalSource, node, PortAlignment.Left ){});
        //}

        return node;
    }

    private void SessionOnDataChanged(object? sender, DataChangedEventArgs e)
    {
        foreach (var signalProcessorConfiguration in e.GetChanges<ISignalProcessorConfiguration>(EChangeType.Added))
        {
           // _ = Diagram.Nodes.Add(CreateSignalProcessorNode(signalProcessorConfiguration, new Point(x, y), new Size(240, 300)));
        }
    }

    private void OnItemDrop(DropEventArgs<DefinitionNodeModel> args)
    {
        var position = Diagram.GetRelativeMousePoint(args.Position.X, args.Position.Y);
        
        var signalProcessorConfiguration = args.Item.CreateItem(position);
        _ = Diagram.Nodes.Add(CreateSignalProcessorNode(signalProcessorConfiguration, position, new Size(240, 300)));
        
    }

    private void OnItemDropEnd()
    {
    }
}