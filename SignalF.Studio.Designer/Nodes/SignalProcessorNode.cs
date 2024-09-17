using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Microsoft.AspNetCore.Components;

namespace SignalF.Studio.Designer.Nodes
{
    public class SignalProcessorNode : NodeModel
    {
        private string _name = "TestName";

        public SignalProcessorNode(string id, Point position, Size size) : base(id, position)
        {
            Size = size;
        }

        [Parameter]
        public string Name
        {
            get => _name;
            set => _name = value;
        }

    }
}
