﻿using Scotec.Blazor.Diagrams.Core.Geometry;

namespace Scotec.Blazor.Diagrams.Core.Models;

public class NodeModel : AreaModel
{
    protected NodeModel(Point position = default, Size size = default) : base(position, size)
    {
    }

    
    protected NodeModel(string id, Point position = default, Size size = default) : base(id, position, size)
    {
    }
}


public abstract class NodeModel<TPortModel> : NodeModel
where TPortModel : PortModel
{
    private readonly List<TPortModel> _ports = [];

    protected NodeModel(Point position = default, Size size = default) : base(position, size)
    {
    }

    protected NodeModel(string id, Point position = default, Size size = default) : base(id, position, size)
    {
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
