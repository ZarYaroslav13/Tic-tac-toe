using Client.Domain.Game;

namespace Client.Domain.Services.ServerService;

public interface IGameService
{
    public void StartGame();

    public GameState Move(int row, int column);
    

}
