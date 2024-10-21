using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using SignalF.Studio.Designer;

namespace SignalF.Studio.Server.Components.Pages;

public partial class Designer
{
    [Inject] protected IJSRuntime JSRuntime { get; set; }

    [Inject] protected NavigationManager NavigationManager { get; set; }

    [Inject] protected DialogService DialogService { get; set; }

    [Inject] protected TooltipService TooltipService { get; set; }

    [Inject] protected ContextMenuService ContextMenuService { get; set; }

    [Inject] protected NotificationService NotificationService { get; set; }

    [Inject] protected DataContext DataContext { get; set; }

    protected override Task OnInitializedAsync()
    {
        base.OnInitializedAsync();

        DataContext.PropertyChanged += (sender, args) => StateHasChanged();

        return Task.CompletedTask;
    }
}
