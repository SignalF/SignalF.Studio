using Blazor.Diagrams;
using Microsoft.AspNetCore.Components;
using SignalF.Datamodel.Calculation;
using SignalF.Datamodel.Hardware;
using SignalF.Studio.Designer.Nodes;

namespace SignalF.Studio.Designer.Components;

public partial class SfToolBox
{
    private readonly BlazorDiagram _blazorDiagram = new();
    private IList<DefinitionNode>? _calculatorDefinitions;

    private IList<DefinitionNode>? _deviceDefinitions;
    private int? _draggedType;

    [Inject] private DocumentManager? DocumentManager { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        if (DocumentManager is null || !DocumentManager.IsOpen)
        {
            return;
        }

        var configuration = DocumentManager.GetConfiguration();
        _deviceDefinitions = configuration.SignalProcessorDefinitions
                                          .OfType<IDeviceDefinition>()
                                          .Select(DefinitionNode (definition) => new DeviceDefinitionNode(definition))
                                          .ToList();

        _calculatorDefinitions = configuration.SignalProcessorDefinitions
                                              .OfType<ICalculatorDefinition>()
                                              .Select(DefinitionNode (definition) => new CalculatorDefinitionNode(definition))
                                              .ToList();

        //LayoutData.Title = "Drag & Drop";
        //LayoutData.Info = "A very simple drag & drop implementation using the HTML5 events.";
        //LayoutData.DataChanged();

        //_blazorDiagram.RegisterComponent<BotAnswerNode, BotAnswerWidget>();
    }
}
