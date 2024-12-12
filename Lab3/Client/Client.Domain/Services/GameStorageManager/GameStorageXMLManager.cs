using Client.Domain.Services.GameService.State;
using Client.Domain.Services.IStorageManager;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Client.Domain.Services.GameStorageManager;

public class GameStorageXMLManager : IGameStorageManager
{
    public GameState LoadGame()
    {
        GameStateXML readedState = new();

        string path = GetPath();
        XmlSerializer serializer = new XmlSerializer(typeof(GameStateXML));
        using (StreamReader reader = new StreamReader(path))
        {
            readedState = (GameStateXML)serializer.Deserialize(reader);
        }
        return readedState.ToGameState();
    }

    public void SaveGame(GameState game)
    {
        throw new NotImplementedException();
    }

    private string GetPath()
    {
        OpenFileDialog fileDialog = new();
        string path = "";

        fileDialog.InitialDirectory = Directory.GetCurrentDirectory() ?? "C:\\";
        fileDialog.DefaultExt = ".csv";
        fileDialog.Filter = "csv files (*.csv)|*.csv";
        fileDialog.FilterIndex = 1;
        fileDialog.RestoreDirectory = true;

        if (fileDialog.ShowDialog() == true)
        {
            path = fileDialog.FileName;
        }

        return path;
    }
}
