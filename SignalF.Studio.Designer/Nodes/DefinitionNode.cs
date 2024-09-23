using Blazor.Diagrams.Core.Geometry;
using SignalF.Datamodel.Signals;

namespace SignalF.Studio.Designer.Nodes;

internal abstract class DefinitionNode
{
    protected DefinitionNode(Guid id, string typeName)
    {
        Id = id;
        TypeName = typeName;
    }

    public Guid Id { get; }
    public string TypeName { get; }

    public ISignalProcessorConfiguration CreateItem(Point position)
    {
        return OnCreateItem(position);
    }

    protected abstract ISignalProcessorConfiguration OnCreateItem(Point position);
}
