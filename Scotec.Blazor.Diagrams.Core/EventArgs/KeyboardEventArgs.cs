namespace Scotec.Blazor.Diagrams.Core.EventArgs;

public record KeyboardEventArgs(string Key, string Code, float Location, bool CtrlKey, bool ShiftKey, bool AltKey);
