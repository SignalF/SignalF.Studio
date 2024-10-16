using Scotec.Blazor.Diagrams.Core.EventArgs;

namespace Scotec.Blazor.Diagrams.EventArgs;

public record BlazorWheelEventArgs(
    double ClientX,
    double ClientY,
    long Button,
    long Buttons,
    bool CtrlKey,
    bool ShiftKey,
    bool AltKey,
    double DeltaX,
    double DeltaY,
    double DeltaZ,
    long DeltaMode) : WheelEventArgs(ClientX, ClientY, Button, Buttons, CtrlKey, ShiftKey, AltKey, DeltaX, DeltaY, DeltaZ, DeltaMode)
{
    public static implicit operator BlazorWheelEventArgs(Microsoft.AspNetCore.Components.Web.WheelEventArgs args)
    {
        return new BlazorWheelEventArgs(args.ClientX, args.ClientY, args.Button, args.Buttons, args.CtrlKey, args.ShiftKey, args.AltKey, args.DeltaX, args.DeltaY, args.DeltaZ, args.DeltaMode);
    }
}
