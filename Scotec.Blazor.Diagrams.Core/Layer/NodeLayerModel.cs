using Scotec.Blazor.Diagrams.Core.Models;

namespace Scotec.Blazor.Diagrams.Core.Layer;

public class NodeLayerModel : LayerModel
{
    private readonly ModelCollection _models = [];

    public void AddNode(NodeModel node)
    {
        _models.Add(node);
    }

    public void AddNodes(IEnumerable<NodeModel> nodes)
    {
        foreach (var node in nodes)
        {
            _models.Add(node);
        }
    }

    public void RemoveNode(NodeModel node)
    {
        _models.Remove(node.Id);
    }

    public void AddLink(LinkModel link)
    {
        _models.Add(link);
    }

    public void AddLinks(IEnumerable<LinkModel> links)
    {
        foreach (var link in links)
        {
            AddLink(link);
        }
    }

    public void RemoveLink(LinkModel link)
    {
        _models.Remove(link.Id);
    }

    public IList<NodeModel> Nodes => _models.OfType<NodeModel>().ToList();

}

public abstract class NodeLayerModel<TNodeModel, TLinkModel> : NodeLayerModel
    where TNodeModel : NodeModel
    where TLinkModel : LinkModel
{
}

