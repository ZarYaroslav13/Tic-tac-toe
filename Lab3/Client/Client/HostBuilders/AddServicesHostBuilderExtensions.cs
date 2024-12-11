using Client.Domain.Services.Settings;
using Client.Domain.Services.Settings.GameSettingsService;
using Client.Domain.Services.Settings.PortSettingsService;
using Client.Presentation.Services.Navigator;
using Client.Presentation.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Client.Presentation.HostBuilders;

internal static class AddServicesHostBuilderExtensions
{
    public static IHostBuilder AddServices(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices(services =>
        {
            services.AddSingleton<INavigator, Navigator>();
            services.AddSingleton<IGameSettingsService, GameSettingsService>();
            services.AddSingleton<IPortSettingsService, PortSettingsService>();
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddScoped<Func<Type, BaseViewModel>>(serviceProvider => viewModelType => (BaseViewModel)serviceProvider.GetRequiredService(viewModelType));
        });

        return hostBuilder;
    }
}
