using Scotec.Blazor.Diagrams.Core.Behaviours;
using Scotec.Blazor.Diagrams.Core.EventArgs;
using Scotec.Blazor.Diagrams.Core.Models;
using Scotec.Extensions.Linq;

namespace Scotec.Blazor.Diagrams.Core.Layer;

public abstract class LayerModel : Model
{
    public DiagramModel Diagram { get; }

    protected LayerModel(DiagramModel diagram)
    {
        Diagram = diagram;
    }

    public override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    public ModelCollection Models { get; } = [];

    protected void AddModel(Model model)
    {
        OnPropertyChanging(nameof(Models));
        Models.Add(model);
        OnPropertyChanged(nameof(Models));
    }

    protected void AddModels(IEnumerable<Model> models)
    {
        OnPropertyChanging(nameof(Models));
        models.ForAll(Models.Add);

        OnPropertyChanged(nameof(Models));
    }

    protected void RemoveModel(Model model)
    {
        OnPropertyChanging(nameof(Models));
        Models.Remove(model.Id);
        OnPropertyChanged(nameof(Models));
    }

    protected void RemoveModels(IEnumerable<Model> models)
    {
        OnPropertyChanging(nameof(Models));
        models.ForAll(model => Models.Remove(model.Id));
        OnPropertyChanged(nameof(Models));
    }

    public IReadOnlyList<TModel> GetModels<TModel>()
        where TModel : Model
    {
        return Models.OfType<TModel>().ToList();
    }

    public event Action<Model?, PointerEventArgs>? PointerDown;
    public event Action<Model?, PointerEventArgs>? PointerUp;
    public event Action<Model?, PointerEventArgs>? PointerEnter;
    public event Action<Model?, PointerEventArgs>? PointerLeave;
    public event Action<Model?, PointerEventArgs>? PointerMove;
    public event Action<Model?, KeyboardEventArgs>? KeyDown;

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

    public virtual void RaiseKeyDownEvent(Model? model, KeyboardEventArgs args)
    {
        KeyDown?.Invoke(model, args);
    }

}
