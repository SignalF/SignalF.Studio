using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Scotec.Blazor.Diagrams.Core.Layer;
using Scotec.Blazor.Diagrams.Core.Models;
using Scotec.Blazor.Diagrams.EventArgs;
using Scotec.Blazor.Diagrams.Widgets;

namespace Scotec.Blazor.Diagrams.Renderer;

public class PortRenderer : Renderer<PortModel>
{
    private const string DiagramPortClass = "diagram-port";
    private ElementReference _element;

    [CascadingParameter] protected DiagramModel DiagramModel { get; set; } = null!;
    [CascadingParameter] protected NodeLayerModel LayerModel { get; set; } = null!;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (!Model.IsVisible)
        {
            return;
        }

        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", string.Join(' ', GetClasses()));
        builder.AddAttribute(2, "model-id", Model.Id);
        builder.AddAttribute(3, "style", $"top: {Model.Position.Y.ToInvariantString()}px; left: {Model.Position.X.ToInvariantString()}px; " +
                                         $"width: {Model.Size.Width.ToInvariantString()}px; height: {Model.Size.Height.ToInvariantString()}px;");


        builder.AddAttribute(10, "onpointerdown", EventCallback.Factory.Create<PointerEventArgs>(this, OnPointerDown));
        builder.AddEventPreventDefaultAttribute(12, "onpointerdown", true);
        builder.AddEventStopPropagationAttribute(11, "onpointerdown", true);
        builder.AddAttribute(20, "onpointerup", EventCallback.Factory.Create<PointerEventArgs>(this, OnPointerUp));
        builder.AddEventStopPropagationAttribute(21, "onpointerup", true);

        //builder.AddAttribute(30, "onmouseenter", EventCallback.Factory.Create<MouseEventArgs>(this, OnMouseEnter));
        //builder.AddAttribute(40, "onmouseleave", EventCallback.Factory.Create<MouseEventArgs>(this, OnMouseLeave));
        //builder.AddAttribute(50, "onpointermove", EventCallback.Factory.Create<PointerEventArgs>(this, OnPointerMove));
        //builder.AddEventPreventDefaultAttribute(12, "onpointermove", true);
        //builder.AddEventStopPropagationAttribute(51, "onpointermove", true);



        builder.AddElementReferenceCapture(100, value => _element = value);
        builder.OpenComponent(101, ComponentRegistration.GetComponentType(Model.GetType()) ?? typeof(ErrorPortWidget));
        builder.AddAttribute(102, "Port", Model);
        builder.CloseComponent();

        builder.CloseElement();
    }

    protected override IList<string> GetClasses()
    {
        return base.GetClasses().InsertIf(0, DiagramPortClass, () => true);
    }

    private bool _pointerDown;
    private void OnPointerDown(PointerEventArgs args)
    {
        DiagramModel.RaisePointerDownEvent(Model, (BlazorPointerEventArgs)args);
    }

    private void OnPointerUp(PointerEventArgs args)
    {
        DiagramModel.RaisePointerUpEvent(Model, (BlazorPointerEventArgs)args);
    }

    private void OnMouseEnter(MouseEventArgs args)
    {
        DiagramModel.RaisePointerEnterEvent(Model, (BlazorPointerEventArgs)args);
    }

    private void OnMouseLeave(MouseEventArgs args)
    {
        DiagramModel.RaisePointerLeaveEvent(Model, (BlazorPointerEventArgs)args);
    }

    private void OnPointerMove(PointerEventArgs args)
    {
        if (_pointerDown)
        {

        }
        DiagramModel.RaisePointerMoveEvent(Model, (BlazorPointerEventArgs)args);
    }

}
