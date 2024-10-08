using Scotec.Blazor.Diagrams.Core.Geometry;
using SignalF.Studio.Designer.Models;

namespace Scotec.Blazor.Diagrams.Core.Models;

public abstract class PortModel: Model 
{
    public NodeModel Parent { get; }
    public PortAlignment Alignment { get; }

    protected PortModel(NodeModel parent, PortAlignment alignment, Point position, Size size ) : base()
    {
        Parent =parent;
        Alignment = alignment;
        Position = position;
        Size = size;
    }

    protected PortModel(string id, NodeModel parent, PortAlignment alignment, Point position, Size size) : base(id)
    {
        Parent = parent;
        Alignment = alignment;
        Position = position;
        Size = size;
    }


    public Point Position { get; set; }
    
    public Size Size { get; set; }
}
