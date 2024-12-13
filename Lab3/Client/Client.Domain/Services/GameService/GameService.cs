using Client.Domain.Services.IStorageManager;
using Client.Domain.Services.ServerService;
using Client.Domain.Services.Settings;
using Client.Domain.Services.Settings.GameSettingsService;
using System.IO.Ports;

namespace Client.Domain.Services.GameService;

public class GameService : IGameService
{
    private readonly ISettingsService _settings;
    private readonly IGameStorageManager _storageManager;
    private Dictionary<GameCommand, Action> GameCommands;
    private GameState _gameState;

    public GameService(IGameStorageManager storageManager, ISettingsService settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        _storageManager = storageManager ?? throw new ArgumentNullException(nameof(storageManager));

        _gameState = new GameState();

        AddImplementationForGameCommands();
    }

    public void InvokeGameCommand(GameCommand command)
    {
        GameCommands[command].Invoke();
    }

    public SerialPort GetServerPort() => _settings.GetPortSettings().ConnectedPort;

    public GameState GetGameState()
    {
        return _gameState;
    }

    public GameState Move(int row, int column)
    {
        if (_gameState.Status != GameStatus.Ongoing)
            return _gameState;

        if (row < GameState.MinCellDimensionValue || column < GameState.MinCellDimensionValue 
         || row > GameState.MaxCellDimensionlValue || column > GameState.MaxCellDimensionlValue)
            throw new ArgumentOutOfRangeException(nameof(row), nameof(column));

        ChangeBoard(row, column);

        return _gameState;
    }

    public bool? IsWinner()
    {
        bool mustDoCheck = _gameState.Board[GameState.MinCellDimensionValue, GameState.MinCellDimensionValue].HasValue;
        for (int i = GameState.MinCellDimensionValue; i < GameState.MaxCellDimensionlValue; i++)
        {


            if (_gameState.Board[i, 0] == _gameState.Board[i, 1] && _gameState.Board[i, 1] == _gameState.Board[i, 2] && _gameState.Board[i, 0].HasValue)
            {
                return _gameState.Board[i, 0];
            }
        }

        for (int j = 0; j < 3; j++)
        {
            if (_gameState.Board[0, j] == _gameState.Board[1, j] && _gameState.Board[1, j] == _gameState.Board[2, j] && _gameState.Board[0, j].HasValue)
            {
                return _gameState.Board[0, j];
            }
        }

        if (_gameState.Board[0, 0] == _gameState.Board[1, 1] && _gameState.Board[1, 1] == _gameState.Board[2, 2] && _gameState.Board[0, 0].HasValue)
        {
            return _gameState.Board[0, 0];
        }
        if (_gameState.Board[0, 2] == _gameState.Board[1, 1] && _gameState.Board[1, 1] == _gameState.Board[2, 0] && _gameState.Board[0, 2].HasValue)
        {
            return _gameState.Board[0, 2];
        }

        return null;
    }

    public void SendRequestForAIMove()
    {
        GetServerPort().Write(_gameState.GetServerBoardString());
    }

    public void AddReceivedEventHandler(SerialDataReceivedEventHandler handler)
    {
        ArgumentNullException.ThrowIfNull(handler);

        _settings.GetPortSettings().AddSerialDataReceivedEventHandler(handler);
    }

    private void AddImplementationForGameCommands()
    {
        GameCommands = new()
        {
            { GameCommand.NewGame,  StartNewGameCommand},
            { GameCommand.LoadGame,  LoadGameCommand},
            { GameCommand.SaveGame,  SaveGameCommand}
        };
    }

    private void ChangeBoard(int row, int column)
    {
        const int maxXNumber = 5;

        if (_gameState.Board[row, column] == null)
            _gameState.Board[row, column] = _gameState.XNumber == _gameState.ONumber;

        if (IsWinner() != null)
        {
            _gameState.Status = (IsWinner() == true) ? GameStatus.WonPlayerX : GameStatus.WonPlayerO;
        }

        if (_gameState.XNumber == maxXNumber && IsWinner() == null)
            _gameState.Status = GameStatus.Draw;
    }

    private void StartNewGameCommand()
    {
        _gameState = new GameState();
        _gameState.Mode = _settings.GetGameSettings().GetGameMode();

        if (_gameState.Mode == GameMode.ManvsAI)
            _gameState.ManPlayer = _settings.GetGameSettings().GetManPlayerSide();
    }

    private void LoadGameCommand()
    {
        try
        {
            _gameState = _storageManager.LoadGame();
            _settings.GetGameSettings().SetManPlayerSide(_gameState.ManPlayer);
            _settings.GetGameSettings().ChangeGameMode(_gameState.Mode);
        }
        catch (Exception)
        {

        }
    }

    private void SaveGameCommand()
    {
        _storageManager.SaveGame(_gameState);
    }
}
