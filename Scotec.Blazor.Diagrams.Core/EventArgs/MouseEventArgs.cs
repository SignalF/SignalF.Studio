namespace Scotec.Blazor.Diagrams.Core.EventArgs;

public record MouseEventArgs(
    double ClientX,
    double ClientY,
    long Button,
    long Buttons,
    bool CtrlKey,
    bool ShiftKey,
    bool AltKey)
{
    public bool IsCancelled { get; set; }
}
