using Scotec.Blazor.Diagrams.Core.Geometry;

namespace Scotec.Blazor.Diagrams.Core.Models;

public class LinkModel : Model
{
    protected LinkModel(Point[]? vertices = default)
    {
        Vertices = vertices ?? [];
    }

    protected LinkModel(string id, Point[]? vertices = default) : base(id)
    {
        Vertices = vertices ?? [];
    }

    public Point[] Vertices { get; }
}
