using Client.Domain.Services.GameService.State;
using Client.Domain.Services.IStorageManager;
using System.IO;
using System.Xml.Serialization;

namespace Client.Domain.Services.GameStorageManager;

public class GameStorageXMLManager : IGameStorageManager
{
    private readonly string _defaultFolder = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Client\Games"));

    public GameState LoadGame()
    {
        GameStateXML readedState = new();
        string f = Directory.GetCurrentDirectory();
        string path = GetPath(_defaultFolder, false);
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
        string path = GetPath(_defaultFolder);

        XmlSerializer serializer = new XmlSerializer(typeof(GameStateXML));
        using (StreamWriter writer = new StreamWriter(path))
        {
            serializer.Serialize(writer, state);
        }
    }

    private string GetPath(string defaultPath = "", bool saveFile = true)
    {
        string path = String.IsNullOrEmpty(defaultPath) ? "C:\\" : defaultPath;

        return saveFile? GetPathToSaveXml(path): GetPathToLoadXml(path);
    }

    public static string GetPathToSaveXml(string defaultPath)
    {
        var saveFileDialog = new SaveFileDialog
        {
            Title = "Save XML File",
            Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*",
            DefaultExt = "xml",
            InitialDirectory = defaultPath
        };

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            return saveFileDialog.FileName;
        }

        return null;
    }

    public static string GetPathToLoadXml(string defaultPath)
    {
        var openFileDialog = new OpenFileDialog
        {
            Title = "Load XML File",
            Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*",
            InitialDirectory = defaultPath
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            return openFileDialog.FileName;
        }

        return null; // User canceled
    }
}
