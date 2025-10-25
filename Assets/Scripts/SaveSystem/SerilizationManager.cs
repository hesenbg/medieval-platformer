using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerilizationManager : MonoBehaviour
{
    public static string path = Application.persistentDataPath + "/saves" + "GameData" + ".save";

    public static bool Save(object SaveData) // path and the object tha will saved
    {
        BinaryFormatter formatter = GetBinaryFormatter();

        // if such directory doesnt exist create one
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        FileStream file = File.Create(path);

        formatter.Serialize(file, SaveData);

        file.Close();

        return true;
    }

    public static object Load()
    {
        if (!File.Exists(path))
        {
            return null;
        }

        BinaryFormatter formatter = GetBinaryFormatter();

        FileStream file = File.Open(path, FileMode.Open);

        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogWarningFormat("failed to reload" + path);
            file.Close() ;
            return null;
        }
    }


    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        return formatter;
    }
}
