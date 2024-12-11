using Client.Domain.Services.ServerService;
using Client.Domain.Services.Settings;
using Client.Domain.Services.Settings.GameSettingsService;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain.Services.GameService;

public class GameService : IGameService
{
    private readonly ISettingsService _settings;
    private GameState _gameState;

    public GameService(ISettingsService settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }

    public void StartGame()
    {
        _gameState = new GameState();
        _gameState.Status = GameStatus.Ongoing;
        _gameState.Mode = _settings.GetGameSettings().GetGameMode();


        if(_gameState.Mode == GameMode.ManvsAI)
            _gameState.ManPlayer = _settings.GetGameSettings().GetManPlayerSide();
    }

    public SerialPort GetServerPort() => _settings.GetPortSettings().ConnectedPort;

    public GameState GetGameState()
    {
        return _gameState;
    }

    public GameState Move(int row, int column)
    {
        if(_gameState.Status != GameStatus.Ongoing)
            return _gameState;

        if(row < 0 || column < 0 || row > 2 || column > 2)
            throw new ArgumentOutOfRangeException(nameof(row), nameof(column));

        ChangeBoard(row, column);

        return _gameState;
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

    public bool? IsWinner()
    {
        for (int i = 0; i < 3; i++)
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

}
