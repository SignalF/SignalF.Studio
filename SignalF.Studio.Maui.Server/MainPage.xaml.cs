using Microsoft.AspNetCore.Components.WebView.Maui;
using SignalF.Studio.Maui.Server.Components.Pages;

namespace SignalF.Studio.Maui.Server
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            var content = new ContentPage(){Title = "Test"};
            var webView = new BlazorWebView(){ HostPage = "wwwroot/index.html" };
            webView.RootComponents.Add(new RootComponent() { Selector = "#app", ComponentType = typeof(Home) });

            content.Content = webView;
            this.Children.Add(content);

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
    }

}
