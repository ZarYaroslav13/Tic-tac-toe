using Client.Domain.Services.Settings.GameSettingsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Client.Domain.Services.GameService.State;

[Serializable]
public class GameStateXML
{
    [XmlArray("Board")]
    [XmlArrayItem("Cell")]
    public bool?[,] Board;
    public GameMode Mode { get; set; }
    public GameStatus Status { get; set; }
    public bool? ManPlayer { get; set; }

    public GameStateXML()
    {
    }

    public GameStateXML(GameState state)
    {
        Board = state.Board.Clone() as bool?[,];
        Mode = state.Mode;
        Status = state.Status;
        ManPlayer = state.ManPlayer;
    }

    public GameState ToGameState()
    {
        GameState state = new();

        state.Board = Board.Clone() as bool?[,];
        state.Mode = Mode;
        state.Status = Status;
        state.ManPlayer = ManPlayer;

        return state;
    }
}
