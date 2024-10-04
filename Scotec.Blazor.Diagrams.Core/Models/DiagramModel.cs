using Scotec.Blazor.Diagrams.Core.Layer;

namespace Scotec.Blazor.Diagrams.Core.Models;

//    public class Diagram<TLayer> where TLayer : LayerBase
public class DiagramModel
{
    public virtual Task OnInitializedAsync()
    {
        return Task.CompletedTask;
    }

    private readonly List<LayerModel> _layers = [];

    public IReadOnlyList<LayerModel> Layers => _layers;

    public void AddLayer(LayerModel layer)
    {
        _layers.Add(layer);
    }
    public void AddLayers(IEnumerable<LayerModel> layers)
    {
        _layers.AddRange(layers);
    }
}
