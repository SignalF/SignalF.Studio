using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazor.Diagrams.Core.Geometry;
using Microsoft.AspNetCore.Components;
using SignalF.Studio.Designer.Model;

namespace SignalF.Studio.Designer.Widgets
{
    public partial class SignalProcessorPort
    {
        [Parameter] public SignalProcessorPortModel Port { get; set; }
        [Parameter] public string Style { get; set; }

        [Parameter] public Point Position { get; set; }

        private string GetPortClasses()
        {
            var type = Port.Type == PortType.SignalSource ? "sf-signal-processor-port-signal-source" : "sf-signal-processor-port-signal-sink";
            return $"sf-signal-processor-port {type}";
        }
    }
}
