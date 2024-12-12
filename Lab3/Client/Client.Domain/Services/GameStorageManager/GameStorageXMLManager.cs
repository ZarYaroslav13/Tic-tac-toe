using Client.Domain.Services.GameService.State;
using Client.Domain.Services.IStorageManager;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Client.Domain.Services.GameStorageManager;

public class GameStorageXMLManager : IGameStorageManager
{
    readonly string defaultFolder = System.IO.Path.GetFullPath(System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Client\Games"));

    public GameState LoadGame()
    {
        GameStateXML readedState = new();
        string f = Directory.GetCurrentDirectory();
        string path = GetPath(defaultFolder, false);
        XmlSerializer serializer = new XmlSerializer(typeof(GameStateXML));
        using (StreamReader reader = new StreamReader(path))
        {
            readedState = (GameStateXML)serializer.Deserialize(reader);
        }
        return readedState.ToGameState();
    }

    public void SaveGame(GameState game)
    {
        GameStateXML state = new(game);
        string path = GetPath(defaultFolder);

        XmlSerializer serializer = new XmlSerializer(typeof(GameStateXML));
        using (StreamWriter writer = new StreamWriter(path))
        {
            serializer.Serialize(writer, this);
        }
    }

    private string GetPath(string defaultPath = "", bool getFolderPath = true)
    {
        string path = String.IsNullOrEmpty(defaultPath) ? "C:\\" : defaultPath;

        return getFolderPath ? GetFolderPath(path) : GetFilePath(path);
    }

    private string GetFolderPath(string defaultPath)
    {
        string path = defaultPath;

        using (var folderDialog = new FolderBrowserDialog())
        {
            folderDialog.SelectedPath = path;

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                path = folderDialog.SelectedPath;
            }
        }

        return path;
    }

    private string GetFilePath(string defaultPath)
    {
        Microsoft.Win32.OpenFileDialog fileDialog = new();
        string path = defaultPath;

        fileDialog.InitialDirectory = path;
        fileDialog.DefaultExt = ".xml";
        fileDialog.Filter = "xml files (*.csv)|*.xml";
        fileDialog.FilterIndex = 1;
        fileDialog.RestoreDirectory = true;

        if (fileDialog.ShowDialog() == true)
        {
            path = fileDialog.FileName;
        }

        return path;
    }
}
