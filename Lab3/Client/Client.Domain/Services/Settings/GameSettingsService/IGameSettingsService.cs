namespace Client.Domain.Services.Settings.GameSettingsService;

public interface IGameSettingsService
{
    public IEnumerable<string> GetAvaiableGameModes();

    public GameMode GetGameMode();

    public void ChangeGameMode(GameMode mode);
}
