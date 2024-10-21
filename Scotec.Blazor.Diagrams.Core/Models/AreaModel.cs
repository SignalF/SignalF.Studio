using Scotec.Blazor.Diagrams.Core.Geometry;

namespace Scotec.Blazor.Diagrams.Core.Models;

public class AreaModel : Model
{
    private Point _position;
    private Size _size;

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

    public Point Position
    {
        get => _position;
        private set => SetProperty(ref _position, value);
    }

    public Size Size
    {
        get => _size;
        private set => SetProperty(ref _size, value);
    }

    public virtual void SetPosition(double x, double y)
    {
        Position = new Point(x, y);
    }

    
    public virtual void SetSize(double width, double height)
    {
        Size = new Size(width, height);
    }
}
