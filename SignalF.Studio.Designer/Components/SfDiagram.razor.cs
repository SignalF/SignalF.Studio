using Blazor.Diagrams;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.PathGenerators;
using Blazor.Diagrams.Core.Routers;
using Blazor.Diagrams.Options;
using Microsoft.AspNetCore.Components;
using SignalF.Datamodel.Configuration;
using SignalF.Studio.Designer.Nodes;

namespace SignalF.Studio.Designer.Components;


public partial class SfDiagram
{
    public BlazorDiagram Diagram { get; set; } = null!;

    [Inject] protected DocumentManager? DocumentManager { get; set; }
    
    //    protected override void OnInitialized()
    protected override Task OnInitializedAsync()
    {
#if !RADZEN
        if (DocumentManager is null )
        {
            return Task.CompletedTask;
        }

        IControllerConfiguration configuration;
        if (DocumentManager.IsOpen)
        {
            configuration = DocumentManager.GetConfiguration();
        }
        else
        {
            using var file = File.OpenRead(@"D:\Projects\scotec\SignalF\SignalF.Studio\TestData\SignalFConfig.xml");
            configuration = DocumentManager.OpenConfiguration(file);
        }

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

        var x = 100;
        var y = 100;

        foreach (var signalProcessor in configuration.SignalProcessorConfigurations)
        {
            _ = Diagram.Nodes.Add(new SignalProcessorNode(configuration: signalProcessor, position: new Point(x, y), size: new Size(240, 300))
            {
                Name = signalProcessor.Name
            });

            x += 100;
            y += 100;
        }



        //var leftPort1 = firstNode.AddPort(new SignalProcessorPort(firstNode, PortAlignment.Left ){});
        //var leftPort1_1 = firstNode.AddPort(new SignalProcessorPort(firstNode, PortAlignment.Left ){});
        //var leftPort1_2 = firstNode.AddPort(new SignalProcessorPort(firstNode, PortAlignment.Left ){});
        //var rightPort1 = firstNode.AddPort(new SignalProcessorPort(firstNode, PortAlignment.Right));

        //var leftPort2 = secondNode.AddPort(new SignalProcessorPort(secondNode, PortAlignment.Left));
        //var rightPort2 = secondNode.AddPort(new SignalProcessorPort(secondNode, PortAlignment.Right));

        //var leftPort = secondNode.AddPort(new SignalProcessorPort());
        //var rightPort = secondNode.AddPort(PortAlignment.Right);
#else
        Diagram = new BlazorDiagram();
#endif

        return Task.CompletedTask;
    }
}