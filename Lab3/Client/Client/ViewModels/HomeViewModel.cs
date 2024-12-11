using Client.Domain.Services;
using Client.Presentation.Services.Navigator;
using System.Windows.Input;

namespace Client.Presentation.ViewModels;

public class HomeViewModel : BaseViewModel
{
    #region Open Settings
    private ICommand _openSettingsCommand = default!;
    public ICommand OpenSettingsCommand => _openSettingsCommand ??= new RelayCommand(OnOpenSettingsCommandExecuted);
    private void OnOpenSettingsCommandExecuted(object o)
    {
        Navigator.NavigateTo<SettingsViewModel>();
    }
    #endregion

    #region Play game
    private ICommand _openGameCommand = default!;
    public ICommand OpenGameCommand => _openGameCommand ??= new RelayCommand(OnOpenGameCommandExecuted);
    private void OnOpenGameCommandExecuted(object o)
    {
        Navigator.NavigateTo<GameViewModel>();
    }
    #endregion
    public HomeViewModel(INavigator navigator) : base(navigator)
    {
    }
}
