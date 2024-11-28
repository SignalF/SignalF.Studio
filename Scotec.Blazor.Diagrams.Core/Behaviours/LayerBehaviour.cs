using Scotec.Blazor.Diagrams.Core.Layer;

namespace Scotec.Blazor.Diagrams.Core.Behaviours;

public abstract class LayerBehaviour<TLayerModel> : ILayerBehaviour
    where TLayerModel : LayerModel
{
    protected LayerBehaviour(TLayerModel layerModel)
    {
        LayerModel = layerModel;
    }

    public TLayerModel LayerModel { get; }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
