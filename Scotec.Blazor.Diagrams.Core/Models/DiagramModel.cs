using System.Reflection;
using Scotec.Blazor.Diagrams.Core.Behaviours;
using Scotec.Blazor.Diagrams.Core.EventArgs;
using Scotec.Blazor.Diagrams.Core.Layer;

namespace Scotec.Blazor.Diagrams.Core.Models;

//    public class Diagram<TLayer> where TLayer : LayerBase
public class DiagramModel : Model
{
    private readonly Func<IEnumerable<LayerModel>> _layerFactory;
    private readonly Func<DiagramModel, IEnumerable<IDiagramBehaviour>> _behavioursFactory;
    private List<IDiagramBehaviour>? _behaviours;

    public DiagramModel(Func<IEnumerable<LayerModel>> layerFactory, Func<DiagramModel, IEnumerable<IDiagramBehaviour>> behavioursFactory)
    {
        _layerFactory = layerFactory;
        _behavioursFactory = behavioursFactory;
    }

    public event Action<Model?, PointerEventArgs>? PointerDown;
    public event Action<Model?, PointerEventArgs>? PointerUp;
    public event Action<Model?, PointerEventArgs>? PointerEnter;
    public event Action<Model?, PointerEventArgs>? PointerLeave;
    public event Action<Model?, PointerEventArgs>? PointerMove;

    public override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _behaviours = _behavioursFactory(this).ToList();

        AddLayers(_layerFactory());

        // Create a list of tasks for initialization
        var initializationTasks = _layers.Select(layer => layer.OnInitializedAsync()).ToList();

        // Wait for all initialization tasks to complete
        await Task.WhenAll(initializationTasks);
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

    public virtual void RaisePointerDownEvent(Model? model, PointerEventArgs args)
    {
        PointerDown?.Invoke(model, args);
    }
    public virtual void RaisePointerUpEvent(Model? model, PointerEventArgs args)
    {
        PointerUp?.Invoke(model, args);
    }
    public virtual void RaisePointerEnterEvent(Model? model, PointerEventArgs args)
    {
        PointerEnter?.Invoke(model, args);
    }
    public virtual void RaisePointerLeaveEvent(Model? model, PointerEventArgs args)
    {
        PointerLeave?.Invoke(model, args);
    }
    public virtual void RaisePointerMoveEvent(Model? model, PointerEventArgs args)
    {
        PointerMove?.Invoke(model, args);
    }
}
