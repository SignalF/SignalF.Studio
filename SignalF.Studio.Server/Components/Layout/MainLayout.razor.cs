using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using SignalF.Studio.Designer;

namespace SignalF.Studio.Server.Components.Layout;

public partial class MainLayout
{
    private bool _sidebarExpanded = true;

    [Inject] protected IJSRuntime JSRuntime { get; set; }

    [Inject] protected NavigationManager NavigationManager { get; set; }

    [Inject] protected DialogService DialogService { get; set; }

    [Inject] protected TooltipService TooltipService { get; set; }

    [Inject] protected ContextMenuService ContextMenuService { get; set; }

    [Inject] protected NotificationService NotificationService { get; set; }

    [Inject] protected DataContext DataContext { get; set; }

    private void SidebarToggleClick()
    {
        _sidebarExpanded = !_sidebarExpanded;
    }

    private void OnSave()
    {
        DataContext.Save();
    }

    private void OnOpen()
    {
        DataContext.Open();
    }

    private void OnClose()
    {
        DataContext.Close();
    }

    private void OnCreate()
    {
        DataContext.Create();
    }
}
