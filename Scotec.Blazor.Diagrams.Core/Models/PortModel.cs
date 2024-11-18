using Scotec.Blazor.Diagrams.Core.Behaviours;
using Scotec.Blazor.Diagrams.Core.Geometry;
using SignalF.Studio.Designer.Models;

namespace Scotec.Blazor.Diagrams.Core.Models;

public abstract class PortModel : AreaModel, IConnectable
{
    private AnchorModel? _anchor;

    protected PortModel(NodeModel node, Point position = default, Size size = default) : base(position, size)
    {
        Node = node;
    }

    protected PortModel(string id, NodeModel parent, Point position = default, Size size = default) : base(id, position, size)
    {
        Node = parent;
    }

    public NodeModel Node { get; }
    public PortAlignment Alignment { get; set; } = PortAlignment.None;

    protected abstract AnchorModel GetAnchor();

    public AnchorModel Anchor => _anchor ??= GetAnchor();
}
