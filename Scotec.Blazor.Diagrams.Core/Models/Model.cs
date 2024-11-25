using Scotec.Blazor.Diagrams.Core.Behaviours;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Scotec.Blazor.Diagrams.Core.Models;

public abstract class Model : ObservableObject, ISelectable
{
    private bool _isSelected;
    private string _title = string.Empty;
    private bool _isVisible = true;
    private bool _isLocked = false;

    public event Action<Model>? Changed;

    protected Model() : this(Guid.NewGuid().ToString("D"))
    {
    }

    protected Model(string id)
    {
        Id = id;
    }

    public string Id { get; }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public bool IsVisible
    {
        get => _isVisible;
        set => SetProperty(ref _isVisible, value);
    }

    public bool IsLocked
    {
        get => _isLocked;
        set => SetProperty(ref _isLocked, value);
    }

    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }

    public virtual Task OnInitializedAsync()
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Fires a <see cref="Changed"/> event.
    /// Typically a model fires a PropertyChanged event that would result in rerendering the associated component.
    /// However, call <see cref="Refresh"/> to force rerendering of the component.
    /// </summary>
    public virtual void Refresh() => Changed?.Invoke(this);

}

