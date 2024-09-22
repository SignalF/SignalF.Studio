using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Scotec.XMLDatabase;
using SignalF.Datamodel.Configuration;
using SignalF.Datamodel.Hardware;

namespace SignalF.Studio.Designer
{
    public class DocumentManager : IDisposable
    {
        private readonly IBusinessDocument _document;
        public bool IsOpen { get; private set; }
        private Guid _sessionId;

        public DocumentManager(IBusinessDocument document)
        {
            _document = document;
            document.Root = "ControllerConfiguration";

            var scheme = PackUriHelper.UriSchemePack;
            document.Schema = new Uri($"{scheme}://application:,,,SignalF.Datamodel;component/Schemas/SignalF.Datamodel.Configuration.xsd");

            using var file = File.OpenRead(@"D:\Projects\scotec\SignalF\SignalF.Studio\TestData\SignalFConfig.xml");
            OpenConfiguration(file);
        }

        public IControllerConfiguration CreateConfiguration()
        {
            if (IsOpen)
            {
                throw new InvalidOperationException("Cannot create new document. Document is alread open.");
            }
            _document.CreateDocument("");
            var session = _document.CreateSession(EBusinessSessionMode.Write);
            _sessionId = session.Id;

            var configuration = session.GetRoot<IControllerConfiguration>();

            // Test code
            configuration.SignalProcessorConfigurations.Create<IDeviceConfiguration>();
            configuration.SignalProcessorConfigurations.Create<IDeviceConfiguration>();

            IsOpen = true;
            return configuration;
        }
        public IControllerConfiguration OpenConfiguration(Stream configuration)
        {
            if (IsOpen)
            {
                throw new InvalidOperationException("Cannot create new document. Document is alread open.");
            }

            _document.OpenDocument(configuration);
            var session = _document.CreateSession(EBusinessSessionMode.Write);

            IsOpen = true;

            return session.GetRoot<IControllerConfiguration>();
        }

        public IControllerConfiguration GetConfiguration()
        {
            if (!IsOpen)
            {
                throw new InvalidOperationException("Cannot get controller configuration. A document must be opened first.");
            }

            return _document.GetSession(_sessionId).GetRoot<IControllerConfiguration>();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                IsOpen = false;
                _document.Close();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
