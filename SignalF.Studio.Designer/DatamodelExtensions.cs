using Microsoft.CodeAnalysis;
using Scotec.Blazor.Diagrams.Core.Geometry;
using SignalF.Datamodel.Designer;
using SignalF.Datamodel.Signals;
using Size = Scotec.Blazor.Diagrams.Core.Geometry.Size;

namespace SignalF.Studio.Designer;

internal static class DatamodelExtensions
{
    /// <summary>
    /// Gets the name of the signal processor definition. If the name is not set, the name of the template is returned.
    /// </summary>
    public static string GetName(this ISignalProcessorDefinition definition)
    {
        var name = definition.Name;
        return string.IsNullOrWhiteSpace(name) ? definition.Template.Name : name;
    }

    public static Point ToPoint(this IPosition position)
    {
        return new Point(position.X, position.Y);
    }

    public static Size ToSize(this ISize size)
    {
        return new Size(size.Width, size.Height);

    }
}
