using Blazor.Diagrams.Core.Geometry;
using SignalF.Datamodel.Calculation;
using SignalF.Datamodel.Configuration;
using SignalF.Datamodel.Signals;

namespace SignalF.Studio.Designer.Nodes;

internal class CalculatorDefinitionNode : DefinitionNode
{
    private readonly ICalculatorDefinition _calculatorDefinition;

    public CalculatorDefinitionNode(ICalculatorDefinition calculatorDefinition)
        : base(calculatorDefinition.Id, calculatorDefinition.GetName())
    {
        _calculatorDefinition = calculatorDefinition;
    }

    protected override ISignalProcessorConfiguration OnCreateItem(Point position)
    {
        var controllerConfiguration = _calculatorDefinition.FindParent<IControllerConfiguration>();

        var session = controllerConfiguration.Session;
        using var transaction = session.CreateTransaction();
        using var notificationLock = session.CreateNotificationLock();

        var deviceConfiguration = controllerConfiguration.SignalProcessorConfigurations.Create<ICalculatorConfiguration>();

        transaction.Commit();

        return deviceConfiguration;
    }
}
