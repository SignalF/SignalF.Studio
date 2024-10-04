using Microsoft.AspNetCore.Components;
using Scotec.Blazor.Diagrams.Core.Models;

namespace Scotec.Blazor.Diagrams;

public class ComponentRegistration
{
    private readonly Dictionary<Type, IComponentMapping> _registrations;

    public ComponentRegistration(IEnumerable<IComponentMapping> mappings)
    {
        _registrations = mappings.ToDictionary(item => item.ModelType, item => item);
    }

    public void RegisterComponent<TModel, TComponent>()
        where TModel : Model
        where TComponent : ComponentBase
    {
        RegisterComponent(typeof(TModel), typeof(TComponent));
    }

    public void Clear()
    {
        _registrations.Clear();
    }

    public void RegisterComponent(Type modelType, Type componentType)
    {
        _registrations.Add(modelType, new ComponentMapping(modelType, componentType));
    }

    public Type? GetComponentType(Type modelType)
    {
        return _registrations.GetValueOrDefault(modelType)?.GetComponentType(modelType);
    }

    public Type? GetComponentType(Model model)
    {
        return GetComponentType(model.GetType());
    }
}

