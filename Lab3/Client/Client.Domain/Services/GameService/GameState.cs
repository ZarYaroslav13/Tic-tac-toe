using Client.Domain.Services.Settings.GameSettingsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain.Services.GameService;

public struct GameState
{
    public int[,] Board;
    public GameMode Mode;
    public GameStatus Status;
    public bool ManPlayer;
}
