using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Scotec.Blazor.Diagrams.Core.Geometry;

namespace Scotec.Blazor.Diagrams;

public static class JsRuntimeExtensions
{
    public static async Task<Rectangle> GetBoundingClientRect(this IJSRuntime jsRuntime, ElementReference element)
    {
        return await jsRuntime.InvokeAsync<Rectangle>("ScotecBlazorDiagrams.getBoundingClientRect", element);
    }

    public static async Task Observe<T>(this IJSRuntime jsRuntime, ElementReference element, DotNetObjectReference<T> reference) where T : class
    {
        try
        {
            await jsRuntime.InvokeVoidAsync("ScotecBlazorDiagrams.observe", element, reference, element.Id);
        }
        catch (ObjectDisposedException)
        {
            // Nothing to do here.
        }
    }

}
