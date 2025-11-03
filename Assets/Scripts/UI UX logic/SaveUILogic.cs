using TMPro;
using UnityEngine;
using System.IO;

public class SaveUILogic : MonoBehaviour
{
    [SerializeField] TMP_Dropdown savedGamesDropdown;

    private void Start()
    {
        RefreshSaveList();
    }

    public void RefreshSaveList()
    {
        string saveDir = Path.Combine(Application.persistentDataPath, "saves");

        if (!Directory.Exists(saveDir))
        {
            Debug.Log("No save directory found.");
            return;
        }

        // Clear previous options before adding new ones
        savedGamesDropdown.ClearOptions();

        string[] saveFiles = Directory.GetFiles(saveDir, "*.data");

        foreach (string filePath in saveFiles)
        {
            // Get only the filename (without directory and extension)
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            savedGamesDropdown.options.Add(new TMP_Dropdown.OptionData(fileName));
        }

        // Refresh the dropdown visually
        savedGamesDropdown.RefreshShownValue();
    }
}
