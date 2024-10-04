using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Scotec.Blazor.Diagrams.Core.Models;

namespace Scotec.Blazor.Diagrams.Renderer;

public abstract class Renderer<TModel> : ComponentBase, IDisposable where TModel : Model
{
    private bool _disposed;
    [Inject] protected IJSRuntime JsRuntime { get; private set; } = null!;

    [Inject] protected ComponentRegistration ComponentRegistration { get; set; } = null!;

    [Parameter] public TModel Model { get; set; } = null!;



    protected virtual IList<string> GetClasses()
    {
        return new List<string>()
            .AddIf(Classes.Hidden, () => !Model.IsVisible)
            .AddIf(Classes.Selected, () => Model.IsSelected)
            .AddIf(Classes.Locked, () => Model.IsLocked);

    }

    protected virtual void Dispose(bool disposing)
    {
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }
        _disposed = true;

        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~Renderer()
    {
        Dispose(false);
    }
}
