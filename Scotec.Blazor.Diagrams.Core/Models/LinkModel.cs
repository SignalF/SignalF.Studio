using System.ComponentModel;
using Scotec.Blazor.Diagrams.Core.Geometry;

namespace Scotec.Blazor.Diagrams.Core.Models;

public class LinkModel : Model
{
    private AnchorModel? _source;
    private AnchorModel? _target;

    protected LinkModel(Point[]? vertices = default)
    {
        Vertices = vertices ?? [];
    }

    protected LinkModel(string id, Point[]? vertices = default) : base(id)
    {
        Vertices = vertices ?? [];
    }

    protected LinkModel(string id, AnchorModel source, AnchorModel target) : base(id)
    {
        _source = source;
        _target = target;
        Vertices = new []
        {
            source.AnchorPoint,
            target.AnchorPoint
        };
    }

    public Point[] Vertices { get; }

    public void Move(Point startPoint, Point endPoint)
    {
        Vertices[0] = startPoint;
        Vertices[^1] = endPoint;

        OnPropertyChanged(nameof(Vertices));
    }

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

    public override void Refresh()
    {
        if (Source != null && Target != null)
        {
            Move(Source.AnchorPoint, Target.AnchorPoint);
        }

        base.Refresh();
    }
}
