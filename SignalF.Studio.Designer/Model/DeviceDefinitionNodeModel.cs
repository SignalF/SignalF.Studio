using Blazor.Diagrams.Core.Geometry;
using SignalF.Datamodel.Configuration;
using SignalF.Datamodel.Hardware;
using SignalF.Datamodel.Signals;

namespace SignalF.Studio.Designer.Model;

internal class DeviceDefinitionNodeModel : DefinitionNodeModel
{
    private readonly IDeviceDefinition _deviceDefinition;

    public DeviceDefinitionNodeModel(IDeviceDefinition deviceDefinition)
        : base(deviceDefinition.Id, deviceDefinition.GetName())
    {
        _deviceDefinition = deviceDefinition;
    }

    protected override ISignalProcessorConfiguration OnCreateItem(Point position)
    {
        var controllerConfiguration = _deviceDefinition.FindParent<IControllerConfiguration>();

        var session = controllerConfiguration.Session;
        using var transaction = session.CreateTransaction();
        using var notificationLock = session.CreateNotificationLock();

        var deviceConfiguration = controllerConfiguration.SignalProcessorConfigurations.Create<IDeviceConfiguration>();
        deviceConfiguration.Definition = _deviceDefinition;

        AddSignalSources(deviceConfiguration);
        AddSignalSinks(deviceConfiguration);

        transaction.Commit();

        return deviceConfiguration;
    }

    private void AddSignalSources(IDeviceConfiguration deviceConfiguration)
    {
        var signalSources = _deviceDefinition.Template.SignalSourceDefinitions.Concat(_deviceDefinition.SignalSourceDefinitions);

        foreach (var signalSourceDefinition in signalSources)
        {
            var signalSource = deviceConfiguration.SignalSources.Create();
            signalSource.Definition = signalSourceDefinition;
            // TODO: Set unit.
            //signalSource.Unit = ...
        }
    }

    private void AddSignalSinks(IDeviceConfiguration deviceConfiguration)
    {
        var signalSinks = _deviceDefinition.Template.SignalSinkDefinitions.Concat(_deviceDefinition.SignalSinkDefinitions);

        foreach (var signalSinkDefinition in signalSinks)
        {
            var signalsink = deviceConfiguration.SignalSinks.Create();
            signalsink.Definition = signalSinkDefinition;
            // TODO: Set unit.
            //signalSink.Unit = ...
        }
    }
}
