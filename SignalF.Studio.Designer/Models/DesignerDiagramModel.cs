using Scotec.Blazor.Diagrams;
using Scotec.Blazor.Diagrams.Core.Behaviours;
using Scotec.Blazor.Diagrams.Core.Geometry;
using Scotec.Blazor.Diagrams.Core.Layer;
using Scotec.Blazor.Diagrams.Core.Models;
using Scotec.Blazor.DragDrop.Components;
using Scotec.XMLDatabase;
using SignalF.Datamodel.Configuration;
using SignalF.Datamodel.Signals;

namespace SignalF.Studio.Designer.Models;

public class DesignerDiagramModel : BlazorDiagramModel
{
    private readonly DataContext _dataContext;

    public DesignerDiagramModel(DataContext dataContext, Func<IEnumerable<LayerModel>> layerFactory, Func<DiagramModel, IEnumerable<IDiagramBehaviour>> behavioursFactory) 
        : base(layerFactory, behavioursFactory)
    {
        _dataContext = dataContext;
    }

    public void OnItemDrop(DropEventArgs<DefinitionNodeModel> args)
    {
        // TODO: This should be done in the configuration layer.
        var position = GetDiagramCanvasMousePoint(args.Position.X, args.Position.Y);
        var signalProcessorConfiguration = args.Item.CreateItem(position);

    }

    public override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

    }


    private void SessionOnDataChanged(object sender, DataChangedEventArgs e)
    {
        foreach (var signalProcessorConfiguration in e.GetChanges<ISignalProcessorConfiguration>(EChangeType.Added))
        {
             //_ = Diagram.Nodes.Add(CreateSignalProcessorNode(signalProcessorConfiguration, new Point(x, y), new Size(240, 300)));
        }
    }

}
