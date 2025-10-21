using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerilizationManager : MonoBehaviour
{


    public static bool Save(string saveName, object SaveData) // path and the object tha will saved
    {
        BinaryFormatter formatter = GetBinaryFormatter();


        // if such directory doesnt exist create one
        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        string path = Application.persistentDataPath + "/saves" + saveName + ".save";

        FileStream file = File.Create(path);

        formatter.Serialize(file, SaveData);

        file.Close();

        return true;
    }



    public static object Load(string Path)
    {
        if (!File.Exists(Path))
        {
            return null;
        }

        BinaryFormatter formatter = GetBinaryFormatter();

        FileStream file = File.Open(Path, FileMode.Open);

        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogWarningFormat("failed to reload" + Path);
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
