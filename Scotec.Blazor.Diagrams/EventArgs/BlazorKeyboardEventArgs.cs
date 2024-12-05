using Scotec.Blazor.Diagrams.Core.EventArgs;

namespace Scotec.Blazor.Diagrams.EventArgs;

internal record BlazorKeyboardEventArgs(string Key, string Code, float Location, bool CtrlKey, bool ShiftKey, bool AltKey)
    : KeyboardEventArgs(Key, Code, Location,CtrlKey, ShiftKey, AltKey)
{
    public static implicit operator BlazorKeyboardEventArgs(Microsoft.AspNetCore.Components.Web.KeyboardEventArgs args)
    {
        return new BlazorKeyboardEventArgs(args.Key, args.Code, args.Location, args.CtrlKey, args.ShiftKey, args.AltKey);
    }
}
