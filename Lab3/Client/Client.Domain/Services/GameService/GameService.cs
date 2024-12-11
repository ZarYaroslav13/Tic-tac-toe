using Client.Domain.Services.ServerService;
using Client.Domain.Services.Settings;
using Client.Domain.Services.Settings.GameSettingsService;
using System;
using System.Collections.Generic;
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

    public GameState GetGameState()
    {
        return _gameState;
    }

    public GameState Move(int row, int column)
    {
        if(row < 0 || column < 0 || row > 2 || column > 2)
            throw new ArgumentOutOfRangeException(nameof(row), nameof(column));

        if (_gameState.Board[row, column] == null)
            _gameState.Board[row, column] = _gameState.XNumber == _gameState.ONumber;

        return _gameState;
    }

    public bool? IsWinner()
    {
        throw new NotImplementedException();
    }
}
