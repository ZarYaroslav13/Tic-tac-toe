namespace Client.Domain.Services.Settings.GameSettingsService;

public class GameSettingsService : IGameSettingsService
{
    public GameMode GameMode { get => GetGameMode(); set => ChangeGameMode(value); }
    private GameMode _currentGameMode;

    public IEnumerable<string> GetAvaiableGameModes() => Enum.GetNames(typeof(GameMode));

    public GameMode GetGameMode()
    {
        return _currentGameMode;
    }

    public void ChangeGameMode(GameMode mode)
    {
        _currentGameMode = mode;
    }
}
