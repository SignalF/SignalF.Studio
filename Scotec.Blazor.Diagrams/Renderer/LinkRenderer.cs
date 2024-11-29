using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Scotec.Blazor.Diagrams.Core.Layer;
using Scotec.Blazor.Diagrams.Core.Models;
using Scotec.Blazor.Diagrams.EventArgs;
using Scotec.Blazor.Diagrams.Widgets;

namespace Scotec.Blazor.Diagrams.Renderer;

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

        var boundaries = Model.Boundaries;

        var top = (boundaries.Top - 10).ToInvariantString();
        var left = (boundaries.Left - 10).ToInvariantString();
        var width = (boundaries.Width + 20).ToInvariantString();
        var height = (boundaries.Height + 20).ToInvariantString();
        var offsetX = ((boundaries.Left - 10) * -1).ToInvariantString();
        var offsetY = ((boundaries.Top - 10) * -1).ToInvariantString();

        builder.OpenElement(0, "svg");
        builder.AddAttribute(1, "class", string.Join(' ', GetClasses()));
        builder.AddAttribute(2, "link-id", Model.Id);
        //builder.AddAttribute(3, "pointer-events", "none");
        builder.AddAttribute(4, "style",
            $"top: {top}px; left: {left}px; width: {width}px; height: {height}px; background-color: transparent;pointer-events: none;");

        builder.OpenElement(5, "g");
        builder.AddAttribute(6, "transform", $"translate({offsetX}, {offsetY})");
        builder.AddAttribute(7, "pointer-events", "none");
        if (!Model.IsDraft)
        {
            builder.AddAttribute(8, "style", "pointer-events: all;cursor: default;");
            builder.AddAttribute(9, "onpointerdown", EventCallback.Factory.Create<PointerEventArgs>(this, OnPointerDown));
            builder.AddEventStopPropagationAttribute(10, "onpointerdown", true);
            builder.AddAttribute(11, "onpointerup", EventCallback.Factory.Create<PointerEventArgs>(this, OnPointerUp));
            builder.AddEventStopPropagationAttribute(12, "onpointerup", true);
        }
        else
        {
            builder.AddAttribute(8, "style", "pointer-events: none;cursor: cross;");
        }
        //builder.AddAttribute(8, "onmouseenter", EventCallback.Factory.Create<MouseEventArgs>(this, OnMouseEnter));
        //builder.AddAttribute(9, "onmouseleave", EventCallback.Factory.Create<MouseEventArgs>(this, OnMouseLeave));
        //builder.AddAttribute(9, "onmousemove", EventCallback.Factory.Create<MouseEventArgs>(this, OnMouseMove));

        builder.AddElementReferenceCapture(13, value => _element = value);
        builder.OpenComponent(14, ComponentRegistration.GetComponentType(Model.GetType()) ?? typeof(ErrorPortWidget));
        builder.AddAttribute(15, "Link", Model);
        builder.CloseComponent();
        builder.CloseElement();

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
