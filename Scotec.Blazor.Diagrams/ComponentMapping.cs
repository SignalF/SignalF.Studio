using Microsoft.AspNetCore.Components;
using Scotec.Blazor.Diagrams.Core.Models;

namespace Scotec.Blazor.Diagrams;

public class ComponentMapping<TModel, TComponent> : IComponentMapping
    where TModel : Model
    where TComponent : ComponentBase
{
    public Type ModelType => typeof(TModel);

    public Type? GetComponentType(Type modelType)
    {
        return typeof(TModel) == modelType ? typeof(TComponent) : null;
    }

    public Type? GetComponentType(Model model)
    {
        return GetComponentType(model.GetType());
    }
}
public class ComponentMapping : IComponentMapping
{
    private readonly Type _componentType;

    public ComponentMapping(Type modelType, Type componentType)
    {
        ModelType = modelType;
        _componentType = componentType;
    }
    public Type ModelType { get; }

    public Type? GetComponentType(Type modelType)
    {
        return ModelType == modelType ? _componentType : null;
    }

    public Type? GetComponentType(Model model)
    {
        return GetComponentType(model.GetType());
    }
}
