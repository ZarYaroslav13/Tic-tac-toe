using Client.Domain.Services;
using Client.Domain.Services.Navigator;
using System.Windows.Input;

namespace Client.Domain.ViewModels;

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
    #endregion
    public HomeViewModel(INavigator navigator) : base(navigator)
    {
    }
}
