using Scotec.Blazor.Diagrams.Core.Geometry;

namespace Scotec.Blazor.Diagrams.Core.Models;

public class PortAnchorModel : AnchorModel
{
    public PortModel Port { get; }

    public PortAnchorModel(PortModel port, Point anchorPoint)
    : base(anchorPoint)
    {
        Port = port;
    }

    protected override Point GetAnchorPoint() => Port.Node.Position + base.GetAnchorPoint();
}
