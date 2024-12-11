using Client.Domain.Services.GameService;

namespace Client.Domain.Services.ServerService;

public interface IGameService
{
    public void StartGame();

    public GameState GetGameState();

    public GameState Move(int row, int column);

    public bool? IsWinner();
}
