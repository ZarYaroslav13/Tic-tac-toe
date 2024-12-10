using Microsoft.Extensions.DependencyInjection;

namespace Client.Domain.ViewModels;

public class ViewModelLocator
{
    private readonly IServiceProvider _serviceProvider;

    public ViewModelLocator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public MainViewModel MainViewModel => _serviceProvider.GetRequiredService<MainViewModel>();
    public HomeViewModel HomeViewModel => _serviceProvider.GetRequiredService<HomeViewModel>();
}
