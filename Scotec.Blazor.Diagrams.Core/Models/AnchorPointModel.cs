using Scotec.Blazor.Diagrams.Core.Geometry;

namespace Scotec.Blazor.Diagrams.Core.Models;

public class AnchorPointModel : Model
{
    private Point _anchorPoint;

    public Point AnchorPoint
    {
        get => _anchorPoint;
        set => SetProperty(ref _anchorPoint, value);
    }
}
