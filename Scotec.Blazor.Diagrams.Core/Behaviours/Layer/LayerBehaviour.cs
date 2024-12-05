using Scotec.Blazor.Diagrams.Core.Layer;

namespace Scotec.Blazor.Diagrams.Core.Behaviours.Layer;

public abstract class LayerBehaviour<TLayerModel> : ILayerBehaviour
    where TLayerModel : LayerModel
{
    protected LayerBehaviour(TLayerModel layerModel)
    {
        LayerModel = layerModel;
    }

    public TLayerModel LayerModel { get; }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
        }
    }
}
