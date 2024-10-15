﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Scotec.Blazor.Diagrams.Core.Layer;
using Scotec.Blazor.Diagrams.Core.Models;
using Scotec.Blazor.Diagrams.EventArgs;
using Scotec.Blazor.Diagrams.Widgets;

namespace Scotec.Blazor.Diagrams.Renderer;

public class NodeRenderer : Renderer<NodeModel>
{
    private const string DiagramNodeClass = "diagram-node";
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

        builder.AddAttribute(4, "onpointerdown", EventCallback.Factory.Create<PointerEventArgs>(this, OnPointerDown));
        builder.AddEventStopPropagationAttribute(5, "onpointerdown", true);
        builder.AddAttribute(6, "onpointerup", EventCallback.Factory.Create<PointerEventArgs>(this, OnPointerUp));
        builder.AddEventStopPropagationAttribute(7, "onpointerup", true);
        builder.AddAttribute(8, "onmouseenter", EventCallback.Factory.Create<MouseEventArgs>(this, OnMouseEnter));
        builder.AddAttribute(9, "onmouseleave", EventCallback.Factory.Create<MouseEventArgs>(this, OnMouseLeave));
        builder.AddAttribute(9, "onmousemove", EventCallback.Factory.Create<MouseEventArgs>(this, OnMouseMove));
        builder.AddElementReferenceCapture(10, value => _element = value);
        builder.OpenComponent(11, ComponentRegistration.GetComponentType(Model.GetType()) ?? typeof(ErrorNodeWidget));
        builder.AddAttribute(12, "Node", Model);
        builder.CloseComponent();

        builder.CloseElement();
    }

    protected override IList<string> GetClasses()
    {
        return base.GetClasses().InsertIf(0, DiagramNodeClass, () => true);
    }

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

    private void OnMouseMove(MouseEventArgs args)
    {
        DiagramModel.RaisePointerMoveEvent(Model, (BlazorPointerEventArgs)args);
    }
}
