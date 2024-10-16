using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Scotec.Blazor.Diagrams.Core.Behaviours;
using Scotec.Blazor.Diagrams.Core.Geometry;
using Scotec.Blazor.Diagrams.EventArgs;
using Scotec.Blazor.Diagrams.Widgets;

namespace Scotec.Blazor.Diagrams.Components;

public class DiagramCanvas : ComponentBase
{
    private ElementReference _elementReference;
    private DotNetObjectReference<DiagramCanvas>? _reference;

    [Inject] protected ComponentRegistration ComponentRegistration { get; set; } = null!;

    [Inject] public IJSRuntime JsRuntime { get; set; } = null!;

    [CascadingParameter] public BlazorDiagramModel BlazorDiagram { get; set; } = null!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _reference = DotNetObjectReference.Create(this);
            BlazorDiagram.SetBounds(await JsRuntime.GetBoundingClientRect(_elementReference));
            await JsRuntime.Observe(_elementReference, _reference!);
        }
    }

    [JSInvokable]
    public void OnResize(Rectangle rect)
    {
        SetContainer(rect);
    }

    public void SetContainer(Rectangle rect)
    {
        BlazorDiagram.SetBounds(rect);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var sequence = 0;

        builder.OpenRegion(sequence++);
        builder.OpenElement(sequence++, "div");
        builder.AddAttribute(sequence++, "class", "diagram-canvas");

        builder.AddAttribute(sequence++, "Model", BlazorDiagram);

        builder.AddAttribute(sequence++, "style", "height: 100%; width: 100%;position: relative;");

        builder.AddAttribute(sequence++, "onpointerdown", EventCallback.Factory.Create<PointerEventArgs>(this, OnPointerDown));
        builder.AddAttribute(sequence++, "onpointerup", EventCallback.Factory.Create<PointerEventArgs>(this, OnPointerUp));
        builder.AddAttribute(sequence++, "onpointermove", EventCallback.Factory.Create<PointerEventArgs>(this, OnPointerMove));
        builder.AddAttribute(sequence++, "onkeydown", EventCallback.Factory.Create<KeyboardEventArgs>(this, OnKeyDown));
        builder.AddAttribute(sequence++, "onwheel", EventCallback.Factory.Create<WheelEventArgs>(this, OnWheel));

        builder.AddEventPreventDefaultAttribute(sequence++, "onpointermove", true);
        builder.AddEventStopPropagationAttribute(sequence++, "onwheel", true);

        builder.AddElementReferenceCapture(sequence++, value => _elementReference = value);

        var zIndex = 1;
        foreach (var layer in BlazorDiagram.Layers)
        {
            builder.OpenRegion(sequence++);

            // Create the CascadingValue for the current layer type. This allows us to use a CascadingParameter of the same type or of a base type.
            builder.OpenComponent(sequence++, typeof(CascadingValue<>).MakeGenericType(layer.GetType()));
            builder.AddAttribute(sequence++, "Value", layer);

            builder.AddAttribute(sequence++, "ChildContent", (RenderFragment)(childBuilder =>
            {
                var top = 0.0;
                var left = 0.0;
                var zoom = 1.0;

                var childSequence = 0;

                if (layer is IMovable movable)
                {
                    top = movable.Position.Y;
                    left = movable.Position.X;
                }

                // ReSharper disable once SuspiciousTypeConversion.Global
                if (layer is IZoomable zoomable)
                {
                    zoom = zoomable.Zoom;
                }

                childBuilder.OpenElement(childSequence++, "div");
                childBuilder.AddAttribute(childSequence++, "class", "diagram-layer");
                childBuilder.AddAttribute(childSequence++, "style",
                    $"z-index: {zIndex++}; height: 100%; width: 100%; position: absolute; top: 0; left: 0; transform-origin: top left; transform: translate({left.ToInvariantString()}px, {top.ToInvariantString()}px) scale({zoom.ToInvariantString()});");

                var layerType = ComponentRegistration.GetComponentType(layer);
                if (layerType is not null)
                {
                    childBuilder.OpenComponent(childSequence++, layerType);
                    childBuilder.AddAttribute(childSequence, "LayerModel", layer);
                }
                else
                {
                    childBuilder.OpenComponent(childSequence, typeof(ErrorLayer));
                }

                childBuilder.CloseComponent();

                childBuilder.CloseElement();
            }));

            builder.CloseComponent();

            builder.CloseRegion();
        }

        builder.CloseElement();
        builder.CloseRegion();
    }

    private void OnPointerDown(PointerEventArgs e)
    {
        BlazorDiagram.RaisePointerDownEvent(null, (BlazorPointerEventArgs)e);
    }

    private void OnPointerMove(PointerEventArgs e)
    {
        BlazorDiagram.RaisePointerMoveEvent(null, (BlazorPointerEventArgs)e);
    }

    private void OnPointerUp(PointerEventArgs e)
    {
        BlazorDiagram.RaisePointerUpEvent(null, (BlazorPointerEventArgs)e);
    }

    private void OnKeyDown(KeyboardEventArgs e)
    {
    }

    private void OnWheel(WheelEventArgs e)
    {
        BlazorDiagram.RaiseWheelEvent((BlazorWheelEventArgs)e);
    }
}
