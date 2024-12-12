using Client.Domain.Services.Settings.GameSettingsService;

namespace Client.Domain.Services.GameService.State;

public struct GameState
{
    public const char CharCellX = 'x';
    public const char CharCellO = 'o';
    public const char CharEmptyCell = ' ';

    public bool?[,] Board;
    public int XNumber => Board.Cast<bool?>().Count(c => c == true);
    public int ONumber => Board.Cast<bool?>().Count(c => c == false);
    public GameMode Mode;
    public GameStatus Status;
    public bool? ManPlayer;

    public GameState()
    {
        Board = new bool?[3, 3];
        Mode = GameMode.None;
        Status = GameStatus.Ongoing;
    }

    public string GetServerBoardString()
    {
        string board = "";
        char cellValue = ' ';
        for (int i = 0; i < Board.GetLength(0); i++)
        {
            for (int j = 0; j < Board.GetLength(1); j++)
            {
                cellValue = Board[i, j] == true ? CharCellX : Board[i, j] == false ? CharCellO : CharEmptyCell;
                board += i.ToString() + j.ToString() + cellValue;
            }
        }
        return board;
    }
}
