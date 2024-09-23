using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using SignalF.Datamodel.Signals;

namespace SignalF.Studio.Designer.Nodes
{
    public class SignalProcessorPort : PortModel
    {
        private readonly ISignalConfiguration _signalConfiguration;

        public SignalProcessorPort(ISignalConfiguration signalConfiguration, NodeModel parent, PortAlignment alignment = PortAlignment.Bottom, Point? position = null, Size? size = null) : this(signalConfiguration, string.Empty, parent, alignment, position, size)
        {
        }

        public SignalProcessorPort(ISignalConfiguration signalConfiguration, string id, NodeModel parent, PortAlignment alignment = PortAlignment.Bottom, Point? position = null, Size? size = null) : base(id, parent, alignment, position, size)
        {
            _signalConfiguration = signalConfiguration;
        }
    }
}
