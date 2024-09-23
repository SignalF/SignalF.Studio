using Blazor.Diagrams;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.PathGenerators;
using Blazor.Diagrams.Core.Routers;
using Blazor.Diagrams.Options;
using Microsoft.AspNetCore.Components;
using Scotec.Blazor.DragDrop.Components;
using Scotec.XMLDatabase;
using SignalF.Datamodel.Configuration;
using SignalF.Datamodel.Hardware;
using SignalF.Datamodel.Signals;
using SignalF.Studio.Designer.Nodes;

namespace SignalF.Studio.Designer.Components;

public partial class SfDiagram
{
    private IList<DefinitionNode> _nodeTypes = new List<DefinitionNode>();

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
                DefaultPathGenerator = new SmoothPathGenerator()
            }
        };
        Diagram = new BlazorDiagram(options);
        Diagram.RegisterComponent<SignalProcessorNode, SignalProcessorWidget>();

        FillDiagram(configuration);
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

    private SignalProcessorNode CreateSignalProcessorNode(ISignalProcessorConfiguration configuration, Point position, Size size)
    {
        var node = new SignalProcessorNode(configuration, position, size)
        {
            Name = configuration.Name
        };

        foreach (var signalSink in configuration.SignalSinks)
        {
            var port = node.AddPort(new SignalProcessorPort(signalSink, node, PortAlignment.Left ){});
        }

        foreach (var signalSource in configuration.SignalSources)
        {
            var port = node.AddPort(new SignalProcessorPort(signalSource, node, PortAlignment.Right ){});
        }

        return node;
    }

    private void SessionOnDataChanged(object? sender, DataChangedEventArgs e)
    {
        foreach (var signalProcessorConfiguration in e.GetChanges<ISignalProcessorConfiguration>(EChangeType.Added))
        {
           // _ = Diagram.Nodes.Add(CreateSignalProcessorNode(signalProcessorConfiguration, new Point(x, y), new Size(240, 300)));
        }
    }

    private void OnItemDrop(DropEventArgs<DefinitionNode> args)
    {
        var position = Diagram.GetRelativeMousePoint(args.Position.X, args.Position.Y);
        
        var signalProcessorConfiguration = args.Item.CreateItem(position);
        _ = Diagram.Nodes.Add(CreateSignalProcessorNode(signalProcessorConfiguration, position, new Size(240, 300)));
        
    }

    private void OnItemDropEnd()
    {
    }
}