using Microsoft.AspNetCore.Components;
using Scotec.Blazor.Diagrams.Core.Layer;
using Scotec.Blazor.Diagrams.Core.Models;

namespace Scotec.Blazor.Diagrams.Components;

public abstract class Layer<TLayerModel> : ComponentBase
    where TLayerModel : LayerModel
{
    [CascadingParameter] protected DiagramModel DiagramModel { get; set; } = null!;

    [Parameter] public TLayerModel LayerModel { get; set; } = null!;


}
