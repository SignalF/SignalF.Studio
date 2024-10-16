using Microsoft.AspNetCore.Components;
using Scotec.Blazor.DragDrop.Components;
using SignalF.Studio.Designer.Models;

namespace SignalF.Studio.Designer.Components;

public partial class DesignerDiagram
{
    private readonly IList<DefinitionNodeModel> _nodeTypes = new List<DefinitionNodeModel>();

    [Inject] protected DesignerDiagramModel DiagramModel { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await DiagramModel.OnInitializedAsync();
    }

    private void OnItemDrop(DropEventArgs<DefinitionNodeModel> args)
    {
        var position = DiagramModel.GetDiagramCanvasMousePoint(args.Position.X, args.Position.Y);

        //_ = Diagram.Nodes.Add(DiagramModel.CreateSignalProcessorNode(args.Item, position));
    }

    private void OnItemDropEnd()
    {
    }
}
