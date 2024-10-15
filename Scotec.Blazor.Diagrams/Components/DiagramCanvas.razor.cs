using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Scotec.Blazor.Diagrams.EventArgs;
using Scotec.Blazor.Diagrams.Widgets;

namespace Scotec.Blazor.Diagrams.Components;

public class DiagramCanvas : ComponentBase
{
    protected ElementReference Element;

    [Inject] protected ComponentRegistration ComponentRegistration { get; set; } = null!;

    [CascadingParameter] public BlazorDiagramModel BlazorDiagram { get; set; } = null!;

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

        builder.AddElementReferenceCapture(sequence++, value => Element = value);

        var zIndex = 1;
        foreach (var layer in BlazorDiagram.Layers)
        {
            var l = layer;
            builder.OpenRegion(sequence++);

            // Create the CascadingValue for the current layer type. This allows us to use a CascadingParameter of the same type or of a base type.
            builder.OpenComponent(sequence++, typeof(CascadingValue<>).MakeGenericType(layer.GetType()));
            builder.AddAttribute(sequence++, "Value", l);

            builder.AddAttribute(sequence++, "ChildContent", (RenderFragment)(childBuilder =>
            {
                var childSequence = 0;
                childBuilder.OpenElement(childSequence++, "div");
                childBuilder.AddAttribute(childSequence++, "class", "diagram-layer");
                childBuilder.AddAttribute(childSequence++, "style", $"z-index: {zIndex++}; height: 100%; width: 100%; position: absolute; top: 0; left: 0;");
                
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
        BlazorDiagram.RaisePointerMoveEvent(null, (BlazorPointerEventArgs)e);
    }
}
