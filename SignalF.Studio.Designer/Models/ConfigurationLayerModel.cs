using Scotec.Blazor.Diagrams.Core.Behaviours;
using Scotec.Blazor.Diagrams.Core.Geometry;
using Scotec.Blazor.Diagrams.Core.Layer;
using Scotec.XMLDatabase;
using Scotec.XMLDatabase.ChangeNotification;
using SignalF.Datamodel.Designer;
using System.Xml.Linq;

namespace SignalF.Studio.Designer.Models;

public class ConfigurationLayerModel : NodeLayerModel<SignalProcessorNodeModel, SignalProcessorLinkModel>
{
    private readonly DataContext _dataContext;
    private readonly Func<ISignalProcessorElement, SignalProcessorNodeModel> _nodeModelFactory;

    public ConfigurationLayerModel(Func<LayerModel, IEnumerable<INodeLayerBehaviour>> behaviours, DataContext dataContext,
                                   Func<ISignalProcessorElement, SignalProcessorNodeModel> nodeModelFactory)
        : base(behaviours)
    {
        _dataContext = dataContext;
        _nodeModelFactory = nodeModelFactory;
    }

    public override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        var configuration = _dataContext.GetConfiguration();
        var elements = configuration.DesignerConfiguration.Elements.OfType<ISignalProcessorElement>();

        CreateSignalProcessorNodes(elements);
        _dataContext.Changed += DataContextOnChanged;
    }

    private void DataContextOnChanged(object sender, DataChangedEventArgs args)
    {
        var newElements = args.GetChanges<ISignalProcessorElement>()
                              .Where(change => change.ChangeType == EChangeNotificationType.Added)
                              .Select(newElement => (ISignalProcessorElement)newElement.BusinessObject);
        CreateSignalProcessorNodes(newElements);

    }

    private void DataContextOnOpened(object sender, EventArgs e)
    {
        var configuration = _dataContext.GetConfiguration();

        var elements = configuration.DesignerConfiguration.Elements.OfType<ISignalProcessorElement>();
        CreateSignalProcessorNodes(elements);

        
    }

    private void DataContextOnClosed(object sender, EventArgs e)
    {
    }

    private void CreateSignalProcessorNodes(IEnumerable<ISignalProcessorElement> elements)
    {
        AddNodes(elements.Select(element =>
        {
            var node = CreateSignalProcessorNode(element);

            return node;
        }));
    }

    private SignalProcessorNodeModel CreateSignalProcessorNode(ISignalProcessorElement designerElement)
    {
        var node = _nodeModelFactory(designerElement);

        return node;
    }
}
