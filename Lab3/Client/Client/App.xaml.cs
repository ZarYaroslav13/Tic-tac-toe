using Client.Domain.ViewModels;
using Client.Presentation.HostBuilders;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Client;

public partial class App : Application
{
    private const string Locator = "Locator";
    private IHost _host;
    public IServiceProvider ServiceProvider { get; private set; }

    public App()
    {
        _host = CreateHostBuilder().Build();
        ServiceProvider = _host.Services;
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        Resources[Locator] = new ViewModelLocator(ServiceProvider);
    }

    private static IHostBuilder CreateHostBuilder(string[] args = null)
    {
        return Host.CreateDefaultBuilder(args)
            .AddConfiguration()
            .AddServices()
            .AddViewModels()
            .AddViews();
    }
}
