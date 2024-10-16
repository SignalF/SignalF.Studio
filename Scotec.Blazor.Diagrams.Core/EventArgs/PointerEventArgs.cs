using Scotec.Blazor.Diagrams.Core.Geometry;

namespace Scotec.Blazor.Diagrams.Core.EventArgs;

public record PointerEventArgs(
    double ClientX,
    double ClientY,
    long Button,
    long Buttons,
    bool CtrlKey,
    bool ShiftKey,
    bool AltKey,
    long PointerId,
    float Width,
    float Height,
    float Pressure,
    float TiltX,
    float TiltY,
    string PointerType,
    bool IsPrimary
)
{
    public bool IsCancelled { get; set; }

    public Point GetClientPoint() => new (ClientX, ClientY);
}
