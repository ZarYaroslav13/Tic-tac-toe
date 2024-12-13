using Client.Domain.Services.GameService;
using Client.Domain.Services.Settings.GameSettingsService;
using System.Xml.Serialization;

namespace Client.Domain.Services.GameStorageManager.XML;

[Serializable]
public class GameStateXML
{
    [XmlArray("Board")]
    [XmlArrayItem("Row")]
    public List<List<bool?>> Board { get; set; }

    public GameMode Mode { get; set; }
    public GameStatus Status { get; set; }
    public bool? ManPlayer { get; set; }

    public GameStateXML()
    {
        Board = new List<List<bool?>>();
    }

    public GameStateXML(GameState state)
    {
        Board = ConvertToNestedList(state.Board);
        Mode = state.Mode;
        Status = state.Status;
        ManPlayer = state.ManPlayer;
    }

    public GameState ToGameState()
    {
        GameState state = new();
        state.Board = ConvertToMultidimensionalArray(Board);
        state.Mode = Mode;
        state.Status = Status;
        state.ManPlayer = ManPlayer;
        return state;
    }

    private static List<List<bool?>> ConvertToNestedList(bool?[,] array)
    {
        var list = new List<List<bool?>>();
        for (int i = 0; i < array.GetLength(0); i++)
        {
            var row = new List<bool?>();
            for (int j = 0; j < array.GetLength(1); j++)
            {
                row.Add(array[i, j]);
            }
            list.Add(row);
        }
        return list;
    }

    private static bool?[,] ConvertToMultidimensionalArray(List<List<bool?>> list)
    {
        if (list.Count == 0 || list[0].Count == 0)
            return new bool?[0, 0];

        int rows = list.Count;
        int cols = list[0].Count;
        var array = new bool?[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                array[i, j] = list[i][j];
            }
        }
        return array;
    }
}
