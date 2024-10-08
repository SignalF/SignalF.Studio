using Scotec.Blazor.Diagrams.Core.Geometry;
using Scotec.Blazor.Diagrams.Core.Models;
using SignalF.Datamodel.Signals;

namespace SignalF.Studio.Designer.Models;

public class SignalProcessorNodeModel : NodeModel<SignalProcessorPortModel>
{
    public SignalProcessorNodeModel(ISignalProcessorConfiguration configuration, Point position, Size size)
        : base(configuration.Id.ToString("D"), position)
    {
        Configuration = configuration;
        Size = size;

        AddPorts();
    }

    //public Size InitialSize { get; }

    public ISignalProcessorConfiguration Configuration { get; }

    public string Name
    {
        get => Configuration.Name;
        set => Configuration.Name = value;
    }

    public string DefinitionName => Configuration.Definition.Name ?? Configuration.Definition.Template.Name;

    private void AddPorts()
    {
        var offsetX = 0.0;
        var offsetY = 20.0;
        foreach (var signalSink in Configuration.SignalSinks)
        {
            var port = new SignalProcessorPortModel(signalSink, this, PortAlignment.Left, new Point(offsetX, offsetY), new Size(10, 20))
            {
            };
            AddPort(port);

            offsetY += 40.0;
        }

        // TODO: Get port width from configuration.
        offsetX = Size.Width - 10.0;
        offsetY = 20.0;
        foreach (var signalSource in Configuration.SignalSources)
        {
            var port = new SignalProcessorPortModel(signalSource, this, PortAlignment.Right, new Point(offsetX, offsetY), new Size(10, 20))
            {
            };
            AddPort(port);

            offsetY += 40.0;
        }
    }
}
