using Scotec.Blazor.Diagrams.Core.Geometry;

namespace Scotec.Blazor.Diagrams.Core.Models;

public class AreaModel : Model
{
    protected AreaModel(Point position = default, Size size = default)
    {
        Position = position;
        Size = size;
    }

    protected AreaModel(string id, Point position = default, Size size = default) : base(id)
    {
        Position = position;
        Size = size;
    }

    public Point Position { get; private set; }

    public Size Size { get; private set; }

    public virtual void SetPosition(double x, double y)
    {
        Position = new Point(x, y);
    }

    public virtual void SetSize(double width, double height)
    {
        Size = new Size(width, height);
    }
}
