using Microsoft.AspNetCore.Components;
using Scotec.Blazor.Diagrams.Core.Layer;

namespace Scotec.Blazor.Diagrams.Components;

public abstract class Layer<TLayerModel> : ComponentBase
    where TLayerModel : LayerModel
{
}
