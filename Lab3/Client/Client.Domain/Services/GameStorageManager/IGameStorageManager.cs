using Client.Domain.Services.GameService.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain.Services.IStorageManager;

public interface IGameStorageManager
{
    public void SaveGame(GameState game);

    public GameState LoadGame();
}
