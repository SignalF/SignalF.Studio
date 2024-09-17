using Blazor.Diagrams;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.PathGenerators;
using Blazor.Diagrams.Core.Routers;
using Blazor.Diagrams.Options;
using SignalF.Studio.Designer.Nodes;

namespace SignalF.Studio.Designer.Components;


public partial class MyDiagram
{
    public BlazorDiagram Diagram { get; set; } = null!;

    protected override void OnInitialized()
    {
#if !RADZEN

        var options = new BlazorDiagramOptions
        {
            AllowMultiSelection = true,
            Zoom =
            {
                Enabled = true,
            },
            Links =
            {
                DefaultRouter = new NormalRouter(),
                DefaultPathGenerator = new SmoothPathGenerator()
            }
        };
        Diagram = new BlazorDiagram(options);
        Diagram.RegisterComponent<SignalProcessorNode, SignalProcessorWidget>();


        var firstNode = Diagram.Nodes.Add(new SignalProcessorNode(id: "Node1", position: new Point(50, 50), size: new Size(240, 300))
        {
            Title = "Node 1"
        });
        var secondNode = Diagram.Nodes.Add(new SignalProcessorNode(id: "Node2", position: new Point(200, 100), size: new Size(240, 300))
        {
            Title = "Node 2"
        });

        var leftPort1 = firstNode.AddPort(new SignalProcessorPort(firstNode, PortAlignment.Left ){});
        var leftPort1_1 = firstNode.AddPort(new SignalProcessorPort(firstNode, PortAlignment.Left ){});
        var leftPort1_2 = firstNode.AddPort(new SignalProcessorPort(firstNode, PortAlignment.Left ){});
        var rightPort1 = firstNode.AddPort(new SignalProcessorPort(firstNode, PortAlignment.Right));

        var leftPort2 = secondNode.AddPort(new SignalProcessorPort(secondNode, PortAlignment.Left));
        var rightPort2 = secondNode.AddPort(new SignalProcessorPort(secondNode, PortAlignment.Right));

        //var leftPort = secondNode.AddPort(new SignalProcessorPort());
        //var rightPort = secondNode.AddPort(PortAlignment.Right);
#else
        Diagram = new BlazorDiagram();
#endif
    }
}