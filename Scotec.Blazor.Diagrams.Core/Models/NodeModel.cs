using System.Collections.Immutable;
using Scotec.Blazor.Diagrams.Core.Behaviours;
using Scotec.Blazor.Diagrams.Core.Geometry;

namespace Scotec.Blazor.Diagrams.Core.Models;

public class NodeModel : AreaModel, IMovable
{
    private readonly List<PortModel> _ports = [];

    protected NodeModel(Point position = default, Size size = default) : base(position, size)
    {
    }

    
    protected NodeModel(string id, Point position = default, Size size = default) : base(id, position, size)
    {
    }

    public bool IsMoving { get; set; }

    public void AddPort(PortModel port)
    {
        _ports.Add(port);
    }

    public void RemovePort(PortModel port)
    {
        _ports.Remove(port);
    }

    public IReadOnlyList<PortModel> GetPorts()
    {
        return _ports.ToImmutableList();
    }

    public IReadOnlyList<TPortModel> GetPorts<TPortModel>()
        where TPortModel : PortModel
    {
        return _ports.OfType<TPortModel>().ToImmutableList();
    }


    public override void SetPosition(double x, double y)
    {
        base.SetPosition(x, y);

        foreach (var port in _ports)
        {
        }
    }

}

