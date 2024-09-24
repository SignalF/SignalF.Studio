using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using SignalF.Datamodel.Signals;

namespace SignalF.Studio.Designer.Nodes;

public class SignalProcessorPortModel : PortModel
{
    private readonly ISignalConfiguration _signalConfiguration;

    public SignalProcessorPortModel(ISignalConfiguration signalConfiguration, NodeModel parent, PortAlignment alignment = PortAlignment.Bottom,
                                    Point? position = null, Size? size = null) : this(signalConfiguration, string.Empty, parent, alignment, position, size)
    {
    }

    public SignalProcessorPortModel(ISignalConfiguration signalConfiguration, string id, NodeModel parent, PortAlignment alignment = PortAlignment.Bottom,
                                    Point? position = null, Size? size = null) : base(id, parent, alignment, position, size)
    {
        _signalConfiguration = signalConfiguration;
    }

    public string? DefinitionName => _signalConfiguration.Definition.Name;

    public string? Name
    {
        get => _signalConfiguration.Name;
        set => _signalConfiguration.Name = value;
    }

    public PortType Type => _signalConfiguration is ISignalSourceConfiguration ? PortType.SignalSource : PortType.SignalSink;

    public double Offset { get; set; }
}

public enum PortType
{
    SignalSink,

    SignalSource
}
