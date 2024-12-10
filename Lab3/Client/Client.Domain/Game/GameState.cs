using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain.Game;

public struct GameState
{
    public int[,] Cells;
    public GameStatus Status;


}
