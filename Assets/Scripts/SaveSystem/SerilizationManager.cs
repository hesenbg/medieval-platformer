using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SerilizationManager
{
    private static string SaveDirectory => Path.Combine(Application.persistentDataPath, "saves");

    public static string GetSavePath(string saveName)
    {
        return Path.Combine(SaveDirectory, saveName + ".data");
    }

    public static void CreateSaveFile(string saveName)
    {
        Directory.CreateDirectory(SaveDirectory);

        string path = GetSavePath(saveName);

        if (!File.Exists(path))
        {
            FileStream file = File.Create(path);
        }
    }

    public static bool SaveData(object saveData, string saveName, bool IsPath)
    {
        if (!Directory.Exists(SaveDirectory))
            return false;

        BinaryFormatter formatter = GetBinaryFormatter();
        string path;
        if (!IsPath)
        {
            path = GetSavePath(saveName);
        }
        else
        {
            path = saveName;
        }
        using (FileStream file = File.Create(path))
        {
            formatter.Serialize(file, saveData);
        }

        return true;
    }

    public static object Load(string saveName, bool IsPath)
    {
        string path;
        if (!IsPath)
        {
            path = GetSavePath(saveName);
        }
        else
        {
            path = saveName;
        }

        if (!File.Exists(path))
            return null;

        BinaryFormatter formatter = GetBinaryFormatter();

        using (FileStream file = File.Open(path, FileMode.Open))
        {
            try
            {
                return formatter.Deserialize(file);
            }
            catch
            {
                Debug.LogWarning($"Failed to load: {path}");
                return null;
            }
        }
    }

    public static void LoadSelectedGame()
    {
        // check if choosen path has data in it 
        object Data = SerilizationManager.Load(FileSelectionManager.SelectedFilePath
            , true);

        if (Data == null)
        {
            SceneManager.LoadSceneAsync(1);
        }
        else
        {
            FileSelectionManager.PlayerData = (SavePlayerData)Data;

            SceneManager.LoadSceneAsync(FileSelectionManager.PlayerData.Level);
        }
    }


    private static BinaryFormatter GetBinaryFormatter() => new BinaryFormatter();
}
