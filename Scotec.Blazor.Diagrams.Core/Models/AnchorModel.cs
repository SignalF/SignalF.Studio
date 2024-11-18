using Scotec.Blazor.Diagrams.Core.Geometry;

namespace Scotec.Blazor.Diagrams.Core.Models;

public class AnchorModel : Model
{
    private readonly Point _anchorPoint;
    public PortModel Port { get; }
    private readonly List<LinkModel> _links = new List<LinkModel>();

    public AnchorModel(PortModel port, Point anchorPoint)
    {
        _anchorPoint = anchorPoint;
        Port = port;
    }

    public Point AnchorPoint => Port.Node.Position + _anchorPoint;

    public IReadOnlyList<LinkModel> Links => _links;

    public void AddLink(LinkModel link)
    {
        _links.Add(link);
    }

    public void RemoveLink(LinkModel link)
    {
        _links.Remove(link);
    }
}
