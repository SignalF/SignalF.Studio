using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Radzen;
using Scotec.Blazor.DragDrop.Components;
using SignalF.Studio.Designer.Module;
using SignalF.Studio.Server.Components;

namespace SignalF.Studio.Server;

internal static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(ConfigureContainer));

// Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddHubOptions(options => options.MaximumReceiveMessageSize = 10 * 1024 * 1024)
        ;

        builder.Services.AddBlazorDragDrop();

        builder.Services.AddControllers();
        builder.Services.AddRadzenComponents();
        builder.Services.AddRadzenCookieThemeService(options =>
        {
            options.Name = "SignalF.Studio.ServerTheme";
            options.Duration = TimeSpan.FromDays(365);
        });
        
        builder.Services.AddHttpClient();
        var app = builder.Build();

// Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.MapControllers();
        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();

    }

    private static void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule(new DatamodelModule());
        builder.RegisterModule(new DiagramModule());
    }
}