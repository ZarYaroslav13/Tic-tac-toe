using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain.Services.GameService;

public enum GameStatus
{
    Ongoing,
    Draw,
    WonPlayerX,
    WonPlayerO
}
