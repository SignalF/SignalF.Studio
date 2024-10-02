using Blazor.Diagrams;
using Microsoft.AspNetCore.Components;
using Scotec.Blazor.DragDrop.Components;
using SignalF.Studio.Designer.Model;

namespace SignalF.Studio.Designer.Components;

public partial class SfDiagram
{
    private IList<DefinitionNodeModel> _nodeTypes = new List<DefinitionNodeModel>();

    public BlazorDiagram Diagram => DiagramModel.Diagram;

    [Inject] protected DiagramModel DiagramModel { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await DiagramModel.OnInitializedAsync();
    }

    private void OnItemDrop(DropEventArgs<DefinitionNodeModel> args)
    {
        var position = Diagram.GetRelativeMousePoint(args.Position.X, args.Position.Y);

        _ = Diagram.Nodes.Add(DiagramModel.CreateSignalProcessorNode(args.Item, position));
    }

    private void OnItemDropEnd()
    {
    }
}
