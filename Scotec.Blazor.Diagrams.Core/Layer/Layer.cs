using Scotec.Blazor.Diagrams.Core.Models;

namespace Scotec.Blazor.Diagrams.Core.Layer;

public abstract class Layer<TNode, TLink> : Model
    where TNode : NodeModel
    where TLink : LinkModel
{
    private readonly ModelCollection _models = [];

    public void AddNode(TNode node)
    {
        _models.Add(node);
    }

    public void RemoveNode(TNode node)
    {
        _models.Remove(node.Id);
    }

    public void AddLink(TLink link)
    {
        _models.Add(link);
    }

    public void RemoveLink(TLink link)
    {
        _models.Remove(link.Id);
    }
}
