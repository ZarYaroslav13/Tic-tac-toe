namespace Client.Domain.Services.Settings.GameSettingsService;

public interface IGameSettingsService
{
    public GameMode GetGameMode();

    public void ChangeGameMode(GameMode mode);
}
