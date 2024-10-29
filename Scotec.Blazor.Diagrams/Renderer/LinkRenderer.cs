using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Xml.Linq;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Scotec.Blazor.Diagrams.Core.Models;
using Scotec.Blazor.Diagrams.Widgets;
using Scotec.Blazor.Diagrams.Core.Layer;
using Scotec.Blazor.Diagrams.EventArgs;

namespace Scotec.Blazor.Diagrams.Renderer
{
    public class LinkRenderer : Renderer<LinkModel>
    {
        private const string DiagramLinkClass = "diagram-link";
        private ElementReference _element;

        [CascadingParameter] protected DiagramModel DiagramModel { get; set; } = null!;
        [CascadingParameter] protected NodeLayerModel LayerModel { get; set; } = null!;

        protected override IList<string> GetClasses()
        {
            return base.GetClasses().InsertIf(0, DiagramLinkClass, () => true);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (!Model.IsVisible)
            {
                return;
            }

            builder.OpenElement(0, "svg");
            builder.AddAttribute(1, "class", string.Join(' ', GetClasses()));
            builder.AddAttribute(2, "link-id", Model.Id);
            builder.AddAttribute(3, "style", $"top: 0; left: 0; width: 100%; height: 100%");

            
            //builder.AddAttribute(4, "onpointerdown", EventCallback.Factory.Create<PointerEventArgs>(this, OnPointerDown));
            //builder.AddEventStopPropagationAttribute(5, "onpointerdown", true);
            //builder.AddAttribute(6, "onpointerup", EventCallback.Factory.Create<PointerEventArgs>(this, OnPointerUp));
            //builder.AddEventStopPropagationAttribute(7, "onpointerup", true);
            //builder.AddAttribute(8, "onmouseenter", EventCallback.Factory.Create<MouseEventArgs>(this, OnMouseEnter));
            //builder.AddAttribute(9, "onmouseleave", EventCallback.Factory.Create<MouseEventArgs>(this, OnMouseLeave));
            //builder.AddAttribute(9, "onmousemove", EventCallback.Factory.Create<MouseEventArgs>(this, OnMouseMove));



            builder.AddElementReferenceCapture(10, value => _element = value);
            builder.OpenComponent(11, ComponentRegistration.GetComponentType(Model.GetType()) ?? typeof(ErrorPortWidget));
            builder.AddAttribute(12, "Link", Model);
            builder.CloseComponent();

            builder.CloseElement();
        }

        private void OnPointerDown(PointerEventArgs args)
        {
            DiagramModel.RaisePointerDownEvent(Model, (BlazorPointerEventArgs)args);
        }

        private void OnPointerUp(PointerEventArgs args)
        {
            DiagramModel.RaisePointerUpEvent(Model, (BlazorPointerEventArgs)args);
        }

    }
}
