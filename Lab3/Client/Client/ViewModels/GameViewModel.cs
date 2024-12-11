using Client.Domain.Services;
using Client.Domain.Services.GameService;
using Client.Domain.Services.ServerService;
using Client.Domain.Services.Settings;
using Client.Presentation.Services.Navigator;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Client.Presentation.ViewModels;

public class GameViewModel : BaseViewModel
{
    public ObservableCollection<ObservableCollection<BitmapImage>> BoardImages { get; set; } = new()
    {
        new ObservableCollection<BitmapImage> { new BitmapImage(), new BitmapImage(), new BitmapImage() },
        new ObservableCollection<BitmapImage> { new BitmapImage(), new BitmapImage(), new BitmapImage() },
        new ObservableCollection<BitmapImage> { new BitmapImage(), new BitmapImage(), new BitmapImage() }
    };

    private readonly IGameService _gameService;

    #region Start new game
    private ICommand _startNewGameCommand = default!;
    public ICommand StartNewGameCommand => _startNewGameCommand ??= new RelayCommand(OnStartNewGameCommandExecuted);
    private void OnStartNewGameCommandExecuted(object o)
    {
        _gameService.StartGame();
        ChangeBoard(_gameService.GetGameState());
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

        ChangeBoard(_gameService.Move(row, column));
    }
    #endregion

    public GameViewModel(INavigator navigator, IGameService service) : base(navigator)
    {
        _gameService = service ?? throw new ArgumentNullException(nameof(service));
    }

    private void ChangeBoard(GameState state)
    {
        var DefaultCellPath = (BitmapImage)Application.Current.Resources["DefaultCell"];
        var CrossCellPath = (BitmapImage)Application.Current.Resources["CrosstCell"];
        var ZeroCellPath = (BitmapImage)Application.Current.Resources["ZeroCell"];

        for(int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                var image = (state.Board[i, j] == null) ? DefaultCellPath : (state.Board[i, j] == true) ? CrossCellPath : ZeroCellPath;
                BoardImages[i][j] = image;
            }
        }
            
    }
}
