using Scotec.Blazor.Diagrams.Core.Behaviours;
using Scotec.Blazor.Diagrams.Core.Geometry;
using SignalF.Studio.Designer.Models;

namespace Scotec.Blazor.Diagrams.Core.Models;

public abstract class PortModel: AreaModel, IConnectable
{
    public NodeModel Parent { get; }
    public PortAlignment Alignment { get; set; } = PortAlignment.None;

    protected PortModel(NodeModel parent, Point position = default, Size size = default) : base(position, size)
    {
        Parent =parent;
    }

    protected PortModel(string id, NodeModel parent, Point position = default, Size size = default) : base(id, position, size)
    {
        Parent = parent;
    }

    public Point AnchorPoint
    {
        get
        {
            var x = Position.X;
            var y = Position.Y + Size.Height / 2.0;

            if (Alignment == PortAlignment.Right)
            {
                x += Size.Width;
            }
            
            return new Point(x, y);
        }
    }
}
