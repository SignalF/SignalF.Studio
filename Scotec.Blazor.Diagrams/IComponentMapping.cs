using Scotec.Blazor.Diagrams.Core.Models;

namespace Scotec.Blazor.Diagrams;

public interface IComponentMapping
{
    Type ModelType { get; }

    Type? GetComponentType(Type modelType);

    Type? GetComponentType(Model model);
}
