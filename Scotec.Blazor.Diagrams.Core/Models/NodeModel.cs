using Scotec.Blazor.Diagrams.Core.Geometry;

namespace Scotec.Blazor.Diagrams.Core.Models;

public class NodeModel : Model
{
    protected NodeModel(string id, Point position) : base(id)
    {
        Position = position;
    }

    public Point Position { get; set; }

    public Size Size { get; set; }
}


public abstract class NodeModel<TPortModel> : NodeModel
where TPortModel : PortModel
{
    private readonly List<TPortModel> _ports = [];

    protected NodeModel(string id, Point position) : base(id, position)
    {
        Position = position;
    }
    public IReadOnlyList<TPortModel> Ports => _ports;

    public void AddPort(TPortModel port)
    {
        _ports.Add(port);
    }

    public void RemovePort(TPortModel port)
    {
        _ports.Remove(port);
    }
}
