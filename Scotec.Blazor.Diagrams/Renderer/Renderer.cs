using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Scotec.Blazor.Diagrams.Core.Models;

namespace Scotec.Blazor.Diagrams.Renderer;

public abstract class Renderer<TModel> : ComponentBase, IDisposable
    where TModel : Model
{
    private bool _disposed;
    private bool _shouldRender;
    private DotNetObjectReference<Renderer<TModel>>? _reference;


    [Inject] protected IJSRuntime JsRuntime { get; private set; } = null!;

    [Inject] protected ComponentRegistration ComponentRegistration { get; set; } = null!;

    [Parameter] public TModel Model { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _reference = DotNetObjectReference.Create(this);
        Model.PropertyChanged += OnModelPropertyChanged;
        Model.Changed += OnModelChanged;

    }

    private void OnModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        ReRender();
    }

    protected override bool ShouldRender()
    {
        if (!_shouldRender)
            return false;

        _shouldRender = false;
        return true;
    }

    protected virtual void OnModelChanged(Model model)
    {
        ReRender();
    }

    private void ReRender()
    {
        _shouldRender = true;
        InvokeAsync(StateHasChanged);

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

    protected virtual IList<string> GetClasses()
    {
        return new List<string>()
               .AddIf(Classes.Hidden, () => !Model.IsVisible)
               .AddIf(Classes.Selected, () => Model.IsSelected)
               .AddIf(Classes.Locked, () => Model.IsLocked);
    }

    protected virtual void Dispose(bool disposing)
    {
        Model.PropertyChanged -= OnModelPropertyChanged;
        _reference?.Dispose();
    }

    ~Renderer()
    {
        Dispose(false);
    }
}
