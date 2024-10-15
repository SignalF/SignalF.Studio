using Scotec.Blazor.Diagrams.Core.Behaviours;
using Scotec.Blazor.Diagrams.Core.Models;

namespace Scotec.Blazor.Diagrams.Core.Layer;

public class NodeLayerModel : LayerModel
{
    public NodeLayerModel(Func<LayerModel, IEnumerable<INodeLayerBehaviour>> behaviours) : base(behaviours)
    {
    }

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

    public IReadOnlyList<NodeModel> GetNodes() => GetModels<NodeModel>().ToList();
    public IReadOnlyList<LinkModel> GetLinks() => GetModels<LinkModel>().ToList();

}

public abstract class NodeLayerModel<TNodeModel, TLinkModel> : NodeLayerModel
    where TNodeModel : NodeModel
    where TLinkModel : LinkModel
{
    protected NodeLayerModel(Func<LayerModel, IEnumerable<INodeLayerBehaviour>> behaviours) : base(behaviours)
    {
    }
}

