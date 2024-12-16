using Client.Domain.Services.GameService;
using Client.Domain.Services.IStorageManager;
using System.IO;
using System.Xml.Serialization;

namespace Client.Domain.Services.GameStorageManager.XML;

/// <summary>
/// Implementation of <see cref="IGameStorageManager"/> that uses XML serialization for saving and loading game states.
/// </summary>
public class GameStorageXMLManager : IGameStorageManager
{
    /// <summary>
    /// Default folder path for saving and loading game files.
    /// </summary>
    private readonly string _defaultFolder = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\Games"));

    /// <summary>
    /// Loads a game state from an XML file.
    /// </summary>
    /// <returns>The loaded <see cref="GameState"/> object.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the XML file cannot be deserialized into a <see cref="GameStateXML"/>.</exception>
    /// <exception cref="FileNotFoundException">Thrown if the file specified by the user does not exist.</exception>
    public GameState LoadGame()
    {
        GameStateXML readedState = new();
        string path = GetPath(_defaultFolder, saveFile: false);
        if (string.IsNullOrEmpty(path))
            return new GameState();

        XmlSerializer serializer = new XmlSerializer(typeof(GameStateXML));
        using (StreamReader reader = new StreamReader(path))
        {
            readedState = (GameStateXML)serializer.Deserialize(reader);
        }
        return readedState.ToGameState();
    }

    /// <summary>
    /// Saves a game state to an XML file.
    /// </summary>
    /// <param name="game">The game state to save.</param>
    /// <exception cref="InvalidOperationException">Thrown if the <see cref="GameState"/> cannot be serialized.</exception>
    public void SaveGame(GameState game)
    {
        GameStateXML state = new(game);
        string path = GetPath(_defaultFolder);

        if (string.IsNullOrEmpty(path))
            return;

        XmlSerializer serializer = new XmlSerializer(typeof(GameStateXML));
        using (StreamWriter writer = new StreamWriter(path))
        {
            serializer.Serialize(writer, state);
        }
    }

    /// <summary>
    /// Retrieves the file path for saving or loading XML files.
    /// </summary>
    /// <param name="defaultPath">The default folder path to use.</param>
    /// <param name="saveFile"><c>true</c> to get a save path, <c>false</c> to get a load path.</param>
    /// <returns>The file path as a string.</returns>
    private string GetPath(string defaultPath = "", bool saveFile = true)
    {
        string path = string.IsNullOrEmpty(defaultPath) ? "C:\\" : defaultPath;
        return saveFile ? GetPathToSaveXml(path) : GetPathToLoadXml(path);
    }

    /// <summary>
    /// Opens a dialog for selecting a file to save as XML.
    /// </summary>
    /// <param name="defaultPath">The default folder path for the dialog.</param>
    /// <returns>The selected file path, or <c>null</c> if the dialog is canceled.</returns>
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

        return null; // User canceled
    }

    /// <summary>
    /// Opens a dialog for selecting an XML file to load.
    /// </summary>
    /// <param name="defaultPath">The default folder path for the dialog.</param>
    /// <returns>The selected file path, or <c>null</c> if the dialog is canceled.</returns>
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
