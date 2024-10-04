using Scotec.Blazor.Diagrams.Core.Models;

namespace Scotec.Blazor.Diagrams.Core.Layer;

public class NodeLayerModel : NodeLayerModel<NodeModel, LinkModel>
{
    private readonly ModelCollection _models = [];

    public void AddNode(NodeModel node)
    {
        _models.Add(node);
    }

    public void RemoveNode(NodeModel node)
    {
        _models.Remove(node.Id);
    }

    public void AddLink(LinkModel link)
    {
        _models.Add(link);
    }

    public void RemoveLink(LinkModel link)
    {
        _models.Remove(link.Id);
    }

    public IList<NodeModel> Nodes => _models.OfType<NodeModel>().ToList();

}

public abstract class NodeLayerModel<TNodeModel, TLinkModel> : LayerModel
    where TNodeModel : NodeModel
    where TLinkModel : LinkModel
{
}

