using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using SignalF.Datamodel.Signals;
using System.Xml.Linq;

namespace SignalF.Studio.Designer.Nodes;

public class SignalProcessorNodeModel : NodeModel
{
    private readonly ISignalProcessorConfiguration _configuration;
    
    private static readonly Size PortSize = new (10, 20);


    public SignalProcessorNodeModel(ISignalProcessorConfiguration configuration, Point position, Size size)
        : base(configuration.Id.ToString("D"), position)
    {
        _configuration = configuration;
        //var x = _configuration.SignalSinks.First();
        Size = size;

        AddPorts();
    }

    private void AddPorts()
    {
        var offset = 20.0;
        foreach (var signalSink in _configuration.SignalSinks)
        {
            var port = new SignalProcessorPortModel(signalSink, this, PortAlignment.Left) { Size = new(10, 20) };
            port.Offset = new Point(0, offset);
            AddPort(port);

            offset += 40.0;
        }

        offset = 20.0;
        foreach (var signalSource in _configuration.SignalSources)
        {
            var port = new SignalProcessorPortModel(signalSource, this, PortAlignment.Right) { Size = new(10, 20) };
            port.Offset = new Point(120,offset);
            AddPort(port);

            offset += 40.0;

        }
    }

    public string? Name
    {
        get => _configuration.Name;
        set => _configuration.Name = value;
    }

    public string? DefinitionName => _configuration.Definition.Name ?? _configuration.Definition.Template.Name;
}
