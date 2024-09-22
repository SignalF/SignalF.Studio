using Blazor.Diagrams;
using Blazor.Diagrams.Core.Geometry;
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
        return new SignalProcessorNode(configuration, position, size)
        {
            Name = configuration.Name
        };
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
        var relativePoint = Diagram.GetRelativePoint(args.Position.X, args.Position.Y);

        var position = new Point((relativePoint.X - Diagram.Pan.X) / Diagram.Zoom,  (relativePoint.Y - Diagram.Pan.Y) / Diagram.Zoom);
        var signalProcessorConfiguration = args.Item.CreateItem(args.Position);
        //_ = Diagram.Nodes.Add(CreateSignalProcessorNode(signalProcessorConfiguration, new Point(args.Position.X, args.Position.Y), new Size(240, 300)));
        _ = Diagram.Nodes.Add(CreateSignalProcessorNode(signalProcessorConfiguration, position, new Size(240, 300)));
        
    }

    private void OnItemDropEnd()
    {
    }
}