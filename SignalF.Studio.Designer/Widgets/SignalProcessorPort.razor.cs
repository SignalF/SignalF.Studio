using System.Globalization;
using Microsoft.AspNetCore.Components;

using SignalF.Studio.Designer.Models;

namespace SignalF.Studio.Designer.Widgets;

public partial class SignalProcessorPort
{
    [Parameter] public SignalProcessorPortModel Port { get; set; }
    [Parameter] public string Style { get; set; }

    private string GetPortClasses()
    {
        var type = Port.Type == PortType.SignalSource ? "sf-signal-processor-port-signal-source" : "sf-signal-processor-port-signal-sink";
        var alignment = Port.Alignment.ToString().ToLowerInvariant();
        return $"sf-signal-processor-port {type} {alignment}";
    }
}
