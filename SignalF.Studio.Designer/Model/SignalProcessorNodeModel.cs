using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using SignalF.Datamodel.Signals;

namespace SignalF.Studio.Designer.Model;

public class SignalProcessorNodeModel : NodeModel
{
    public SignalProcessorNodeModel(ISignalProcessorConfiguration configuration, Point position, Size size)
        : base(configuration.Id.ToString("D"), position)
    {
        Configuration = configuration;
        InitialSize = size;

        AddPorts();
    }

    public Size InitialSize { get; }

    public ISignalProcessorConfiguration Configuration { get; }

    public string Name
    {
        get => Configuration.Name;
        set => Configuration.Name = value;
    }

    public string DefinitionName => Configuration.Definition.Name ?? Configuration.Definition.Template.Name;

    private void AddPorts()
    {
        var offset = 20.0;
        foreach (var signalSink in Configuration.SignalSinks)
        {
            var port = new SignalProcessorPortModel(signalSink, this, PortAlignment.Left, new Point(0, offset), new Size(10, 20))
            {
                //Size = new(10, 20),
                Offset = new Point(0, offset)
            };
            AddPort(port);

            offset += 40.0;
        }


        offset = 20.0;
        foreach (var signalSource in Configuration.SignalSources)
        {
            var port = new SignalProcessorPortModel(signalSource, this, PortAlignment.Right, new Point(120, offset), new Size(10, 20))
            {
                //Size = new(10, 20),
                Offset = new Point(120, offset)
            };
            AddPort(port);

            offset += 40.0;
        }
    }
}
