using Scotec.Blazor.Diagrams.Core.Behaviours;
using Scotec.Blazor.Diagrams.Core.EventArgs;
using Scotec.Blazor.Diagrams.Core.Geometry;
using Scotec.Blazor.Diagrams.Core.Layer;
using Scotec.Extensions.Linq;

namespace Scotec.Blazor.Diagrams.Core.Models;

//    public class Diagram<TLayer> where TLayer : LayerBase
public class DiagramModel : Model
{
    private readonly Func<DiagramModel, IEnumerable<IDiagramBehaviour>> _behavioursFactory;
    private readonly Func<DiagramModel, IEnumerable<LayerModel>> _layerFactory;

    private readonly List<LayerModel> _layers = [];
    private List<IDiagramBehaviour>? _behaviours;
    private Rectangle _bounds = new();

    public DiagramModel(Func<DiagramModel, IEnumerable<LayerModel>> layerFactory,
                        Func<DiagramModel, IEnumerable<IDiagramBehaviour>> behavioursFactory)
    {
        _layerFactory = layerFactory;
        _behavioursFactory = behavioursFactory;
    }

    public Rectangle Bounds
    {
        get => _bounds;
        private set => SetProperty(ref _bounds, value);
    }

    public IReadOnlyList<LayerModel> Layers => _layers;

    public double Zoom { get; set; } = 1.0;

    public Point Pan { get; set; }

    public event Action<Model?, PointerEventArgs>? PointerDown;
    public event Action<Model?, PointerEventArgs>? PointerUp;
    public event Action<Model?, PointerEventArgs>? PointerEnter;
    public event Action<Model?, PointerEventArgs>? PointerLeave;
    public event Action<Model?, PointerEventArgs>? PointerMove;
    public event Action<Model?, KeyboardEventArgs>? KeyDown;
    public event Action<WheelEventArgs>? Wheel;

    public void SetBounds(Rectangle bounds)
    {
        if (bounds.Equals(Bounds))
        {
            return;
        }

        Bounds = bounds;
    }

    public Point GetRelativeMousePoint(double clientX, double clientY)
    {
        return new Point(clientX - Bounds.Left - Pan.X /* / Zoom*/, clientY - Bounds.Top - Pan.Y /*/ Zoom*/);
    }

    public Point GetDiagramCanvasMousePoint(double clientX, double clientY)
    {
        return new Point((clientX - Bounds.Left - Pan.X) / Zoom, (clientY - Bounds.Top - Pan.Y) / Zoom);
    }

    public override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _behaviours = _behavioursFactory(this).ToList();

        AddLayers(_layerFactory(this));

        // Create a list of tasks for initialization
        var initializationTasks = _layers.Select(layer => layer.OnInitializedAsync()).ToList();

        // Wait for all initialization tasks to complete
        await Task.WhenAll(initializationTasks);
    }

    public void AddLayer(LayerModel layer)
    {
        if (layer is IZoomable zoomable)
        {
            zoomable.Zoom = Zoom;
        }

        _layers.Add(layer);
    }

    public void AddLayers(IEnumerable<LayerModel> layers)
    {
        layers.OfType<IZoomable>().ForAll(zoomable => zoomable.Zoom = Zoom);
        _layers.AddRange(layers);
    }

    public virtual void RaisePointerDownEvent(Model? model, PointerEventArgs args)
    {
        _layers.ForAll(layer => layer.RaisePointerDownEvent(model, args));

        PointerDown?.Invoke(model, args);
    }

    public virtual void RaisePointerUpEvent(Model? model, PointerEventArgs args)
    {
        _layers.ForAll(layer => layer.RaisePointerUpEvent(model, args));

        PointerUp?.Invoke(model, args);
    }

    public virtual void RaisePointerEnterEvent(Model? model, PointerEventArgs args)
    {
        _layers.ForAll(layer => layer.RaisePointerEnterEvent(model, args));

        PointerEnter?.Invoke(model, args);
    }

    public virtual void RaisePointerLeaveEvent(Model? model, PointerEventArgs args)
    {
        _layers.ForAll(layer => layer.RaisePointerLeaveEvent(model, args));

        PointerLeave?.Invoke(model, args);
    }

    public virtual void RaisePointerMoveEvent(Model? model, PointerEventArgs args)
    {
        _layers.ForAll(layer => layer.RaisePointerMoveEvent(model, args));

        PointerMove?.Invoke(model, args);
    }

    public virtual void RaiseKeyDownEvent(Model? model, KeyboardEventArgs args)
    {
        _layers.ForAll(layer => layer.RaiseKeyDownEvent(model, args));

        KeyDown?.Invoke(model, args);
    }

    public virtual void RaiseWheelEvent(WheelEventArgs args)
    {
        Wheel?.Invoke(args);
    }
}
