using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;

namespace SignalF.Studio.Designer.Nodes
{
    public class SignalProcessorPort : PortModel
    {
        public SignalProcessorPort(NodeModel parent, PortAlignment alignment = PortAlignment.Bottom, Point position = null, Size size = null) : base(parent, alignment, position, size)
        {
        }

        public SignalProcessorPort(string id, NodeModel parent, PortAlignment alignment = PortAlignment.Bottom, Point position = null, Size size = null) : base(id, parent, alignment, position, size)
        {
        }
    }
}
