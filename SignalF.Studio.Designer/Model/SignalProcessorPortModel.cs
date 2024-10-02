using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.Models.Base;
using SignalF.Datamodel.Signals;

namespace SignalF.Studio.Designer.Model;

public class SignalProcessorPortModel : PortModel
{
    public ISignalConfiguration SignalConfiguration { get; }

    public SignalProcessorPortModel(ISignalConfiguration signalConfiguration, NodeModel parent, PortAlignment alignment = PortAlignment.Bottom,
                                    Point position = null, Size size = null) : this(signalConfiguration, signalConfiguration.Id.ToString("D"), parent, alignment, position, size)
    {
    }

    public SignalProcessorPortModel(ISignalConfiguration signalConfiguration, string id, NodeModel parent, PortAlignment alignment = PortAlignment.Bottom,
                                    Point position = null, Size size = null) : base(id, parent, alignment, position, size)
    {
        SignalConfiguration = signalConfiguration;
    }

    public string DefinitionName => SignalConfiguration.Definition.Name;

    public string Name
    {
        get => SignalConfiguration.Name;
        set => SignalConfiguration.Name = value;
    }

    public PortType Type => SignalConfiguration is ISignalSourceConfiguration ? PortType.SignalSource : PortType.SignalSink;

    public Point Offset { get; set; }

    public override bool CanAttachTo(ILinkable other)
    {
        if (other is not SignalProcessorPortModel port || port.Id == Id)
        {
            return false;
        }
        
        return Type != port.Type;
    }
}

public enum PortType
{
    SignalSink,

    SignalSource
}
