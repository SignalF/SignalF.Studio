namespace Scotec.Blazor.Diagrams.Core.Models;

public abstract class NodeModel : Model
{
    private readonly List<PortModel> _ports = [];
    public IReadOnlyList<PortModel> Ports => _ports;

    public void AddPort(PortModel port)
    {
        _ports.Add(port);
    }

    public void RemovePort(PortModel port)
    {
        _ports.Remove(port);
    }
}
