using Scotec.Blazor.Diagrams.Core.EventArgs;

namespace Scotec.Blazor.Diagrams.EventArgs;

internal record BlazorPointerEventArgs(
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
    bool IsPrimary)
    : PointerEventArgs(ClientX, ClientY,
        Button, Buttons, CtrlKey, ShiftKey, AltKey,
        PointerId, Width, Height, Pressure, TiltX, TiltY, PointerType, IsPrimary)
{
    public static implicit operator BlazorPointerEventArgs(Microsoft.AspNetCore.Components.Web.PointerEventArgs args)
    {
        return new BlazorPointerEventArgs(args.ClientX, args.ClientY, args.Button, args.Buttons, args.CtrlKey, args.ShiftKey, args.AltKey, args.PointerId, args.Width, args.Height, args.Pressure
        , args.TiltX, args.TiltY, args.PointerType, args.IsPrimary);
    }

    public static implicit operator BlazorPointerEventArgs(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
    {
        return new BlazorPointerEventArgs(args.ClientX, args.ClientY, args.Button, args.Buttons, args.CtrlKey, args.ShiftKey, args.AltKey, 0, 0, 0, 0, 0, 0, string.Empty, false);
    }

}
