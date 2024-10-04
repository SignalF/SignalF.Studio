using Microsoft.AspNetCore.Components;
using SignalF.Datamodel.Calculation;
using SignalF.Datamodel.Hardware;
using SignalF.Studio.Designer.Models;

namespace SignalF.Studio.Designer.Components;

public partial class SfToolBox
{
    private IList<DefinitionNodeModel> _calculatorDefinitions;

    private IList<DefinitionNodeModel> _deviceDefinitions;

    [Inject] private DocumentManager DocumentManager { get; set; }

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
                                          .Select(DefinitionNodeModel (definition) => new DeviceDefinitionNodeModel(definition))
                                          .ToList();

        _calculatorDefinitions = configuration.SignalProcessorDefinitions
                                              .OfType<ICalculatorDefinition>()
                                              .Select(DefinitionNodeModel (definition) => new CalculatorDefinitionNodeModel(definition))
                                              .ToList();

    }
}
