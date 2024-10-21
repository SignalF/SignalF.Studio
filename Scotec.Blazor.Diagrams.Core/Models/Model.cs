using Microsoft.Toolkit.Mvvm.ComponentModel;
using Scotec.Blazor.Diagrams.Core.Behaviours;

namespace Scotec.Blazor.Diagrams.Core.Models;

public abstract class Model : ObservableObject, ISelectable
{
    private bool _isSelected;
    private string _title = string.Empty;
    private bool _isVisible = true;
    private bool _isLocked = false;

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
}

