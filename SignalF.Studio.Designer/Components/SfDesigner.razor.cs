using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using SignalF.Studio.Designer.Models;

namespace SignalF.Studio.Designer.Components
{
    public partial class SfDesigner
    {
        [Inject] private DesignerModel DesignerModel { get; set; }

        [Inject] protected IJSRuntime JsRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }
    }
}