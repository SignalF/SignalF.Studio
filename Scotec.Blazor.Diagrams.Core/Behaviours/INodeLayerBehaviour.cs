using Scotec.Blazor.Diagrams.Core.Layer;

namespace Scotec.Blazor.Diagrams.Core.Behaviours;

public interface INodeLayerBehaviour : ILayerBehaviour
{
    public delegate IEnumerable<INodeLayerBehaviour> Factory(NodeLayerModel nodeLayerModel);
}
