using Scotec.Blazor.Diagrams.Core.Geometry;
using SignalF.Datamodel.Calculation;
using SignalF.Datamodel.Configuration;
using SignalF.Datamodel.Designer;
using SignalF.Datamodel.Hardware;
using SignalF.Datamodel.Signals;

namespace SignalF.Studio.Designer.Models;

internal class CalculatorDefinitionNodeModel : DefinitionNodeModel
{
    private readonly ICalculatorDefinition _calculatorDefinition;

    public CalculatorDefinitionNodeModel(ICalculatorDefinition calculatorDefinition)
        : base(calculatorDefinition.Id, calculatorDefinition.GetName())
    {
        _calculatorDefinition = calculatorDefinition;
    }

    protected override ISignalProcessorConfiguration OnCreateItem(Point position)
    {
        // TODO: Same code in DeviceDefinitionNodeModel. Create generic base class.
        var controllerConfiguration = _calculatorDefinition.FindParent<IControllerConfiguration>();

        var session = controllerConfiguration.Session;
        using var transaction = session.CreateTransaction();
        using var notificationLock = session.CreateNotificationLock();

        var calculatorConfiguration = controllerConfiguration.SignalProcessorConfigurations.Create<ICalculatorConfiguration>();
        calculatorConfiguration.Definition = _calculatorDefinition;

        AddSignalSources(calculatorConfiguration);
        AddSignalSinks(calculatorConfiguration);

        var calculatorElement = controllerConfiguration.DesignerConfiguration.Elements.Create<ISignalProcessorElement>();
        calculatorElement.SignalProcessor = calculatorConfiguration;
        calculatorElement.Position.X = position.X;
        calculatorElement.Position.Y = position.Y;
        calculatorElement.Size.Width = 240.0;
        calculatorElement.Size.Height = 300.0;

        transaction.Commit();

        return calculatorConfiguration;
    }

    private void AddSignalSources(ICalculatorConfiguration calculatorConfiguration)
    {
        var signalSources = _calculatorDefinition.Template.SignalSourceDefinitions.Concat(_calculatorDefinition.SignalSourceDefinitions);

        foreach (var signalSourceDefinition in signalSources)
        {
            var signalSource = calculatorConfiguration.SignalSources.Create();
            signalSource.Definition = signalSourceDefinition;
            // TODO: Set unit.
            //signalSource.Unit = ...
        }
    }

    private void AddSignalSinks(ICalculatorConfiguration calculatorConfiguration)
    {
        var signalSinks = _calculatorDefinition.Template.SignalSinkDefinitions.Concat(_calculatorDefinition.SignalSinkDefinitions);

        foreach (var signalSinkDefinition in signalSinks)
        {
            var signalsink = calculatorConfiguration.SignalSinks.Create();
            signalsink.Definition = signalSinkDefinition;
            // TODO: Set unit.
            //signalSink.Unit = ...
        }
    }

}
