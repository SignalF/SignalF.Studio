using Scotec.Blazor.Diagrams.Core.EventArgs;

namespace Scotec.Blazor.Diagrams.EventArgs;

internal record BlazorMouseEventArgs(double ClientX, double ClientY, long Button, long Buttons, bool CtrlKey, bool ShiftKey, bool AltKey)
    : MouseEventArgs(ClientX, ClientY, Button, Buttons, CtrlKey, ShiftKey, AltKey)
{
    public static implicit operator BlazorMouseEventArgs(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
    {
        return new BlazorMouseEventArgs(args.ClientX, args.ClientY, args.Button, args.Buttons, args.CtrlKey, args.ShiftKey, args.AltKey);
    }
}
