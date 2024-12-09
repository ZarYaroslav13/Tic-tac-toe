using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain.Services.Settings.GameSettingsService;

public class GameSettingsService : IGameSettingsService
{
    public GameMode GameMode { get => GetGameMode(); set => ChangeGameMode(value); }
    private GameMode _currentGameMode;

    public void ChangeGameMode(GameMode mode)
    {
        _currentGameMode = mode;
    }

    public GameMode GetGameMode()
    {
        return _currentGameMode;
    }
}
