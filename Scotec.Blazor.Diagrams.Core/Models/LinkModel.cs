using Scotec.Blazor.Diagrams.Core.Geometry;

namespace Scotec.Blazor.Diagrams.Core.Models;

public class LinkModel : Model
{
    private AnchorModel? _source;
    private AnchorModel? _target;

    protected LinkModel(string id, AnchorModel source, AnchorModel target) : base(id)
    {
        _source = source;
        _target = target;
        Vertices = new[]
        {
            source.AnchorPoint,
            target.AnchorPoint
        };

        Boundaries = CalculateBoundaries();

    }

    public Point[] Vertices { get; }

    public AnchorModel? Source
    {
        get => _source;
        set => SetProperty(ref _source, value);
    }

    public AnchorModel? Target
    {
        get => _target;
        set => SetProperty(ref _target, value);
    }

    public Rectangle Boundaries { get; private set; }

    public void Move(Point startPoint, Point endPoint)
    {
        Vertices[0] = startPoint;
        Vertices[^1] = endPoint;

        Boundaries = CalculateBoundaries();

        OnPropertyChanged(nameof(Vertices));
    }

    public override void Refresh()
    {
        if (Source != null && Target != null)
        {
            Move(Source.AnchorPoint, Target.AnchorPoint);
        }

        base.Refresh();
    }

    private Rectangle CalculateBoundaries()
    {
        var minX = Vertices.Min(p => p.X);
        var maxX = Vertices.Max(p => p.X);
        var minY = Vertices.Min(p => p.Y);
        var maxY = Vertices.Max(p => p.Y);

        return new Rectangle(minX, minY, maxX - minX, maxY - minY);
    }
}
