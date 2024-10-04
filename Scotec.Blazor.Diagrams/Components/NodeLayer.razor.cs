using Microsoft.AspNetCore.Components;
using Scotec.Blazor.Diagrams.Core.Layer;
using Scotec.Blazor.Diagrams.Core.Models;

namespace Scotec.Blazor.Diagrams.Components;

public partial class NodeLayer : NodeLayer<NodeLayerModel, NodeModel, LinkModel>
{
}

//public class NodeLayer<TModel, TLink> : ComponentBase
public class NodeLayer<TLayerModel, TNodeModel, TLinkModel> : Layer<TLayerModel>
    where TLayerModel : NodeLayerModel<TNodeModel, TLinkModel>
    where TNodeModel : NodeModel 
    where TLinkModel : LinkModel
{
    [CascadingParameter] protected DiagramModel DiagramModel { get; set; } = null!;
    [CascadingParameter] protected TLayerModel LayerModel { get; set; } = null!;

    protected override Task OnInitializedAsync()
    {
        return base.OnInitializedAsync();
    }
}
