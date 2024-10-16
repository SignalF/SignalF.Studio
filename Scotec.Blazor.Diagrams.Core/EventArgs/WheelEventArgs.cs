using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scotec.Blazor.Diagrams.Core.EventArgs
{
    public record WheelEventArgs(
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
        long DeltaMode) : MouseEventArgs(ClientX, ClientY, Button, Buttons, CtrlKey, ShiftKey, AltKey);

}
