using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scotec.XMLDatabase.ChangeNotification;
using SignalF.Datamodel.Configuration;
using SignalF.Datamodel.Hardware;
using SignalF.Datamodel.Signals;

namespace SignalF.Studio.Designer.Nodes
{
    internal class DeviceDefinitionNode : DefinitionNode
    {
        private readonly IDeviceDefinition _deviceDefinition;

        public DeviceDefinitionNode(IDeviceDefinition deviceDefinition) 
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

            transaction.Commit();

            return deviceConfiguration;
        }
    }
}
