using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerilizationManager : MonoBehaviour
{
    public static SerilizationManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private static string SaveDirectory => Path.Combine(Application.persistentDataPath, "saves");

    private static string GetSavePath(string saveName)
    {
        return Path.Combine(SaveDirectory, saveName + ".data");
    }

    public static void CreateSaveFile(string saveName)
    {
        if (!Directory.Exists(SaveDirectory))
            Directory.CreateDirectory(SaveDirectory);

        string path = GetSavePath(saveName);

        if (!File.Exists(path))
        {
            using (FileStream file = File.Create(path)) { }
        }
    }

    public bool SaveData(object saveData, string saveName)
    {
        if (!Directory.Exists(SaveDirectory))
            Directory.CreateDirectory(SaveDirectory);

        BinaryFormatter formatter = GetBinaryFormatter();
        string path = GetSavePath(saveName);

        using (FileStream file = File.Create(path))
        {
            formatter.Serialize(file, saveData);
        }

        return true;
    }

    public static object Load(string saveName)
    {
        string path = GetSavePath(saveName);

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

    private static BinaryFormatter GetBinaryFormatter() => new BinaryFormatter();
}
