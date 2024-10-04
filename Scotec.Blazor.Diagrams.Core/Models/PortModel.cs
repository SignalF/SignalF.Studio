using Scotec.Blazor.Diagrams.Core.Geometry;

namespace Scotec.Blazor.Diagrams.Core.Models;

public abstract class PortModel : Model
{
    public Point Position { get; set; }
    
    public Size Size { get; set; }
}
