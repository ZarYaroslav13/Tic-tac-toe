using Client.Domain.Services.Settings;
using Client.Presentation.Services.Navigator;

namespace Client.Presentation.ViewModels;

public class GameViewModel : BaseViewModel
{
    private readonly ISettingsService _settings;

    public GameViewModel(INavigator navigator, ISettingsService settings) : base(navigator)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }
}
