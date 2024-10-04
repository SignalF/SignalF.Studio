using Scotec.Blazor.Diagrams.Core.Geometry;

namespace Scotec.Blazor.Diagrams.Core.Models;

public abstract class NodeModel : Model
{
    private readonly List<PortModel> _ports = [];

    protected NodeModel(string id, Point position) : base(id)
    {
        Position = position;
    }
    public IReadOnlyList<PortModel> Ports => _ports;

    public Point Position { get; set; }

    public Size Size { get; set; }

    public void AddPort(PortModel port)
    {
        _ports.Add(port);
    }

    public void RemovePort(PortModel port)
    {
        _ports.Remove(port);
    }
}
