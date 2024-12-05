using System.Collections.Immutable;
using Scotec.Blazor.Diagrams.Core.Models;
using SignalF.Datamodel.Designer;
using SignalF.Datamodel.Signals;
using SignalF.Studio.Designer.Models;
using System.Xml.Linq;

namespace SignalF.Studio.Designer.Services;

public class DomainService
{
    private readonly DataContext _dataContext;
    private readonly SignalProcessorLinkModel.Factory _linkModelFactory;
    private readonly SignalProcessorNodeModel.Factory _nodeModelFactory;

    public DomainService(DataContext dataContext,
                         SignalProcessorNodeModel.Factory nodeModelFactory,
                         SignalProcessorLinkModel.Factory linkModelFactory)
    {
        _dataContext = dataContext;
        _nodeModelFactory = nodeModelFactory;
        _linkModelFactory = linkModelFactory;
    }

    public void DeleteConnections(LinkModel links)
    {

    }

    public void CreateLink(SignalProcessorPortModel source, SignalProcessorPortModel target)
    {
        var session = _dataContext.GetConfiguration().Session;
        using var changeLock = session.CreateNotificationLock();
        using var transaction = session.CreateTransaction();

        var configuration = _dataContext.GetConfiguration();
        var connection = configuration.Connections.Create();
        connection.SignalSource = (ISignalSourceConfiguration)source.SignalConfiguration;
        connection.SignalSink = (ISignalSinkConfiguration)target.SignalConfiguration;

        var linkElement = configuration.DesignerConfiguration.Elements.Create<ILinkElement>();
        linkElement.Connection = connection;

        transaction.Commit();
    }

    public IEnumerable<SignalProcessorNodeModel> GetAllSignalProcessors()
    {
        var configuration = _dataContext.GetConfiguration();
        var nodeElements = configuration.DesignerConfiguration.Elements.OfType<ISignalProcessorElement>();

        return GetSignalProcessorNodes(nodeElements);
    }

    private IEnumerable<SignalProcessorNodeModel> GetSignalProcessorNodes(IEnumerable<ISignalProcessorElement> elements)
    {
        foreach (var element in elements)
        {
            yield return CreateSignalProcessorNode(element);
        }
    }

    private SignalProcessorNodeModel CreateSignalProcessorNode(ISignalProcessorElement designerElement)
    {
        return _nodeModelFactory(designerElement);
    }


}
