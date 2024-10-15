using Scotec.Blazor.Diagrams.Core.Geometry;

namespace Scotec.Blazor.Diagrams.Core.Behaviours;

public interface IMovable
{
    Point Position { get; }

    void SetPosition(double x, double y);
}
