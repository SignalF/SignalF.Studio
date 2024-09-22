using SignalF.Datamodel.Signals;

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
}
