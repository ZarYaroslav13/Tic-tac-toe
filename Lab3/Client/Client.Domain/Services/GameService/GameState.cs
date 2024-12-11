﻿using Client.Domain.Services.Settings.GameSettingsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain.Services.GameService;

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
        Board = new bool?[3,3];
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
                cellValue = (Board[i,j] == true) ? CharCellX : (Board[i, j] == false) ? CharCellO : CharEmptyCell;   
                board += i + j + cellValue;
            }
        }
        return board;
    }
}
