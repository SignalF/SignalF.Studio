using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scotec.Blazor.Diagrams.Core.Models;
using Microsoft.AspNetCore.Components.Web;
using System.Xml.Linq;

namespace Scotec.Blazor.Diagrams.Renderer
{
    public class NodeRenderer : Renderer
    {
        [Parameter] public NodeModel Node { get; set; } = null!;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (!Node.Visible)
                return;

            var componentType = BlazorDiagram.GetComponent(Node) ??
                                (_isSvg ? typeof(SvgNodeWidget) : typeof(NodeWidget));
            var classes = new StringBuilder("diagram-node")
                .AppendIf(" locked", Node.Locked)
                .AppendIf(" selected", Node.Selected)
                .AppendIf(" grouped", Node.Group != null);

            builder.OpenElement(0, _isSvg ? "g" : "div");
            builder.AddAttribute(1, "class", classes.ToString());
            builder.AddAttribute(2, "data-node-id", Node.Id);

            if (_isSvg)
            {
                builder.AddAttribute(3, "transform",
                    $"translate({Node.Position.X.ToInvariantString()} {Node.Position.Y.ToInvariantString()})");
            }
            else
            {
                builder.AddAttribute(3, "style",
                    $"top: {Node.Position.Y.ToInvariantString()}px; left: {Node.Position.X.ToInvariantString()}px");
            }

            builder.AddAttribute(4, "onpointerdown", EventCallback.Factory.Create<PointerEventArgs>(this, OnPointerDown));
            builder.AddEventStopPropagationAttribute(5, "onpointerdown", true);
            builder.AddAttribute(6, "onpointerup", EventCallback.Factory.Create<PointerEventArgs>(this, OnPointerUp));
            builder.AddEventStopPropagationAttribute(7, "onpointerup", true);
            builder.AddAttribute(8, "onmouseenter", EventCallback.Factory.Create<MouseEventArgs>(this, OnMouseEnter));
            builder.AddAttribute(9, "onmouseleave", EventCallback.Factory.Create<MouseEventArgs>(this, OnMouseLeave));
            builder.AddElementReferenceCapture(10, value => _element = value);
            builder.OpenComponent(11, componentType);
            builder.AddAttribute(12, "Node", Node);
            builder.CloseComponent();

            builder.CloseElement();

        }
    }
}
