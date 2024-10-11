using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Scotec.Blazor.Diagrams.Core.Models;
using Scotec.Blazor.Diagrams.Widgets;

namespace Scotec.Blazor.Diagrams.Renderer;

public class PortRenderer : Renderer<PortModel>
{
    private const string DiagramPortClass = "diagram-port";
    private ElementReference _element;

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

        builder.AddElementReferenceCapture(10, value => _element = value);
        builder.OpenComponent(11, ComponentRegistration.GetComponentType(Model.GetType()) ?? typeof(ErrorPortWidget));
        builder.AddAttribute(12, "Port", Model);
        builder.CloseComponent();

        builder.CloseElement();
    }

    protected override IList<string> GetClasses()
    {
        return base.GetClasses().InsertIf(0, DiagramPortClass, () => true);
    }
}
