using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using Scotec.Blazor.Diagrams.Components;
using Scotec.Blazor.Diagrams.Core.Layer;

namespace Scotec.Blazor.Diagrams;

public static class Extensions
{
    public static IList<T> AddIf<T>(this IList<T> list, T item, Func<bool> condition)
    {
        if (condition())
        {
            list.Add(item);
        }

        return list;
    }

    public static IList<T> InsertIf<T>(this IList<T> list, int index, T item, Func<bool> condition)
    {
        if (condition())
        {
            list.Insert(index, item);
        }

        return list;
    }

    public static string ToInvariantString(this double number)
    {
        return number.ToString(CultureInfo.InvariantCulture);
    }

    public static IServiceCollection AddBlazorDiagrams(this IServiceCollection services)
    {
        return services.AddScoped<ComponentRegistration>()
                       .AddSingleton<IComponentMapping, ComponentMapping<NodeLayerModel, NodeLayer>>();
    }
}
