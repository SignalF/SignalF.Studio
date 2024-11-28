using Scotec.Blazor.Diagrams.Core.Geometry;

namespace Scotec.Blazor.Diagrams.Core.Models;

public class AnchorModel : Model
{
    private readonly Point _anchorPoint;
    private readonly List<LinkModel> _links = new List<LinkModel>();

    public AnchorModel(Point anchorPoint)
    {
        _anchorPoint = anchorPoint;
    }

    public Point AnchorPoint => GetAnchorPoint();

    public IReadOnlyList<LinkModel> Links => _links;

    protected virtual Point GetAnchorPoint() => _anchorPoint;

    public void AddLink(LinkModel link)
    {
        _links.Add(link);
    }

    public void RemoveLink(LinkModel link)
    {
        _links.Remove(link);
    }
}
