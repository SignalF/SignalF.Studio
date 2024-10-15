using Microsoft.Extensions.DependencyInjection;
using Scotec.Blazor.Diagrams.Core.Behaviours;
using Scotec.Blazor.Diagrams.Core.EventArgs;
using Scotec.Blazor.Diagrams.Core.Models;

namespace Scotec.Blazor.Diagrams.Core.Layer;

public abstract class LayerModel : Model
{
    private readonly ModelCollection _models = [];

    protected LayerModel(Func<LayerModel, IEnumerable<ILayerBehaviour>> behaviours)
    {
        var x = behaviours(this).ToList();
    }

    protected void AddModel(Model model)
    {
        _models.Add(model);
    }

    protected void AddModels(IEnumerable<Model> models)
    {
        foreach (var model in models)
        {
            _models.Add(model);
        }
    }

    public void RemoveModel(Model model)
    {
        _models.Remove(model.Id);
    }

    public IReadOnlyList<TModel> GetModels<TModel>() where TModel : Model
    { 
        return _models.OfType<TModel>().ToList();
    }


    public event Action<Model?, PointerEventArgs>? PointerDown;
    public event Action<Model?, PointerEventArgs>? PointerUp;
    public event Action<Model?, PointerEventArgs>? PointerEnter;
    public event Action<Model?, PointerEventArgs>? PointerLeave;


    public virtual void RaisePointerDownEvent(Model model, PointerEventArgs args)
    {
        PointerDown?.Invoke(model, args);
    }
    public virtual void RaisePointerUpEvent(Model model, PointerEventArgs args)
    {
        PointerUp?.Invoke(model, args);
    }
    public virtual void RaisePointerEnterEvent(Model model, PointerEventArgs args)
    {
        PointerEnter?.Invoke(model, args);
    }
    public virtual void RaisePointerLeaveEvent(Model model, PointerEventArgs args)
    {
        PointerLeave?.Invoke(model, args);
    }

}
