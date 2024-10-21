using Microsoft.AspNetCore.Components;
using SignalF.Datamodel.Calculation;
using SignalF.Datamodel.Hardware;
using SignalF.Studio.Designer.Models;

namespace SignalF.Studio.Designer.Components;

public partial class SfToolBox
{
    private IList<DefinitionNodeModel> _calculatorDefinitions = new List<DefinitionNodeModel>();
    private IList<DefinitionNodeModel> _deviceDefinitions = new List<DefinitionNodeModel>();

    [Inject] private DataContext DataContext { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        
        DataContext.Opened += DataContextOnOpened;
        DataContext.Closed += DataContextOnClosed;
    }

    private void DataContextOnClosed(object sender, EventArgs e)
    {
        _deviceDefinitions.Clear();
        _calculatorDefinitions.Clear();
        StateHasChanged();
    }

    private void DataContextOnOpened(object sender, EventArgs e)
    {
        var configuration = DataContext.GetConfiguration();
        _deviceDefinitions = configuration.SignalProcessorDefinitions
                                          .OfType<IDeviceDefinition>()
                                          .Select(DefinitionNodeModel (definition) => new DeviceDefinitionNodeModel(definition))
                                          .ToList();

        _calculatorDefinitions = configuration.SignalProcessorDefinitions
                                              .OfType<ICalculatorDefinition>()
                                              .Select(DefinitionNodeModel (definition) => new CalculatorDefinitionNodeModel(definition))
                                              .ToList();

        StateHasChanged();
    }
}
