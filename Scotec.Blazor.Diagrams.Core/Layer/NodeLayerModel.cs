using Scotec.Blazor.Diagrams.Core.Behaviours;
using Scotec.Blazor.Diagrams.Core.Geometry;
using Scotec.Blazor.Diagrams.Core.Models;

namespace Scotec.Blazor.Diagrams.Core.Layer;

public class NodeLayerModel : LayerModel, IMovable, IZoomable
{
    public NodeLayerModel(Func<LayerModel, IEnumerable<INodeLayerBehaviour>> behaviours) : base(behaviours)
    {
    }

    public Point Position { get; private set; }

    public virtual void SetPosition(double x, double y)
    {
        Position = new Point(x, y);
    }

    public bool IsMoving { get; set; }

    public double Zoom { get; set; } = 1.5;

    public void AddNode(NodeModel node)
    {
        AddModel(node);
    }

    public void AddNodes(IEnumerable<NodeModel> nodes)
    {
        AddModels(nodes);
    }

    public void RemoveNode(NodeModel node)
    {
        RemoveModel(node);
    }

    public void AddLink(LinkModel link)
    {
        AddModel(link);
    }

    public void AddLinks(IEnumerable<LinkModel> links)
    {
        AddModels(links);
    }

    public void RemoveLink(LinkModel link)
    {
        RemoveModel(link);
    }

    public IReadOnlyList<NodeModel> GetNodes()
    {
        return GetModels<NodeModel>().ToList();
    }

    public IReadOnlyList<LinkModel> GetLinks()
    {
        return GetModels<LinkModel>().ToList();
    }
}

public abstract class NodeLayerModel<TNodeModel, TLinkModel> : NodeLayerModel
    where TNodeModel : NodeModel
    where TLinkModel : LinkModel
{
    protected NodeLayerModel(Func<LayerModel, IEnumerable<INodeLayerBehaviour>> behaviours) : base(behaviours)
    {
    }
}
