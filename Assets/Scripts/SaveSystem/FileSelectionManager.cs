using System.IO;
using UnityEngine;
public static class FileSelectionManager 
{
    public static string SelectedFilePath;

    public static SavePlayerData PlayerData;

    public static string GetCurrentPath()
    {
        return SelectedFilePath;
    }
}