using Scotec.Blazor.Diagrams.Core.Models;

namespace Scotec.Blazor.Diagrams.Core.Layer;

public abstract class LayerBase : Model
{
    private readonly ModelCollection _models = [];

    public void AddNode<TNode>(TNode node)
        where TNode : NodeModel
    {
        _models.Add(node);
    }

    public void RemoveNode<TNode>(TNode node)
        where TNode : NodeModel
    {
        _models.Remove(node.Id);
    }

    public void AddLink<TLink>(TLink link)
        where TLink : LinkModel
    {
        _models.Add(link);
    }

    public void RemoveLink<TLink>(TLink link)
        where TLink : LinkModel
    {
        _models.Remove(link.Id);
    }
}
