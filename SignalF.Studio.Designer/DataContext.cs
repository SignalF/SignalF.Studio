using System.IO.Packaging;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Scotec.XMLDatabase;
using SignalF.Datamodel.Configuration;

namespace SignalF.Studio.Designer;

public class DataContext : ObservableObject, IDisposable
{
    private readonly Func<IBusinessDocument> _documentFactory;
    private IBusinessDocument _document;
    private bool _isOpen;
    private Guid _sessionId;

    private const string FileName = @"D:\Projects\scotec\SignalF\SignalF.Studio\TestData\SignalFConfig.xml";

    public DataContext(Func<IBusinessDocument> documentFactory)
    {
        _documentFactory = documentFactory;
    }

    public bool IsOpen
    {
        get => _isOpen;
        private set => SetProperty(ref _isOpen, value);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public event EventHandler Opened;

    public event EventHandler Closed;

    public event EventHandler<DataChangedEventArgs> Changed;

    public void Save()
    {
        using var file = File.Open(FileName, FileMode.Create, FileAccess.Write);
        _document.SaveDocument(file);
        file.Close();
    }

    public void Open()
    {
        PrepareDocument();

        using var file = File.OpenRead(FileName);
        OpenConfiguration(file);
        Opened?.Invoke(this, EventArgs.Empty);
    }

    private void PrepareDocument()
    {
        _document = _documentFactory();
        _document.Root = "ControllerConfiguration";

        var scheme = PackUriHelper.UriSchemePack;
        _document.Schema = new Uri($"{scheme}://application:,,,SignalF.Datamodel;component/Schemas/SignalF.Datamodel.Configuration.xsd");
    }

    public void Close()
    {
        if (!IsOpen)
        {
            return;
        }

        // _document.GetSession(_sessionId).DataChanged -= SessionOnDataChanged;

        _document.Close();
        _document = null;
        IsOpen = false;
        Closed?.Invoke(this, EventArgs.Empty);
    }

    public void Create()
    {
        PrepareDocument();

        CreateConfiguration();

        Opened?.Invoke(this, EventArgs.Empty);
    }

    public IControllerConfiguration CreateConfiguration()
    {
        if (IsOpen)
        {
            throw new InvalidOperationException("Cannot create new document. Document is already open.");
        }

        _document.CreateDocument("");
        var session = _document.CreateSession(EBusinessSessionMode.Write);
        _sessionId = session.Id;

        session.DataChanged += SessionOnDataChanged;

        IsOpen = true;
        return session.GetRoot<IControllerConfiguration>();
    }

    private void SessionOnDataChanged(object sender, DataChangedEventArgs args)
    {
        Changed?.Invoke(this, args);
    }

    public IControllerConfiguration OpenConfiguration(Stream configuration)
    {
        if (IsOpen)
        {
            throw new InvalidOperationException("Cannot create new document. Document is alread open.");
        }

        _document.OpenDocument(configuration);
        var session = _document.CreateSession(EBusinessSessionMode.Write);
        _sessionId = session.Id;
        
        session.DataChanged += SessionOnDataChanged;

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
            Close();
        }
    }
}
