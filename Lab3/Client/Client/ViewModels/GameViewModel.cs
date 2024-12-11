using Client.Domain.Services;
using Client.Domain.Services.ServerService;
using Client.Domain.Services.Settings;
using Client.Presentation.Services.Navigator;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.Presentation.ViewModels;

public class GameViewModel : BaseViewModel
{
    private readonly IGameService _gameService;

    #region Start new game
    private ICommand _startNewGameCommand = default!;
    public ICommand StartNewGameCommand => _startNewGameCommand ??= new RelayCommand(OnOpenHomeCommandExecuted);
    private void OnOpenHomeCommandExecuted(object o)
    {
        
    }
    #endregion

    public GameViewModel(INavigator navigator, IGameService service) : base(navigator)
    {
        _gameService = service ?? throw new ArgumentNullException(nameof(service));
    }
}
