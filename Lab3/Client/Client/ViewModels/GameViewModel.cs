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
    public ICommand StartNewGameCommand => _startNewGameCommand ??= new RelayCommand(OnStartNewGameCommandExecuted);
    private void OnStartNewGameCommandExecuted(object o)
    {
        _gameService.StartGame();
    }
    #endregion

    #region Try move
    private ICommand _cellClickedCommand = default!;
    public ICommand CellClickedCommand => _cellClickedCommand ??= new RelayCommand(OnCellClickedCommandExecuted);
    private void OnCellClickedCommandExecuted(object o)
    {
        if (o == null || o.GetType() != typeof(string))
            throw new ArgumentException(nameof(o));

        string move = o as string;
        int row = (int)char.GetNumericValue(move[0]);
        int column = (int)char.GetNumericValue(move[1]);
    }
    #endregion

    public GameViewModel(INavigator navigator, IGameService service) : base(navigator)
    {
        _gameService = service ?? throw new ArgumentNullException(nameof(service));
    }
}
