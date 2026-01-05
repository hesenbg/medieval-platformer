using TMPro;
using UnityEngine;
using System.IO;
public class SaveUILogic : MonoBehaviour
{
    [SerializeField] TMP_Dropdown savedGamesDropdown;

    private void OnEnable()
    {
        RefreshSaveList();
    }

    private void Start()
    {
        RefreshSaveList();
    }

    private void Update()
    {
        
    }

    public string[] GetSavePaths()
    {
        string saveDir = Path.Combine(Application.persistentDataPath, "saves");

        if (!Directory.Exists(saveDir))
        {
            Debug.Log("No save directory found.");
            return null;
        }

        // Clear previous options before adding new ones
        savedGamesDropdown.ClearOptions();

        string[] saveFiles = Directory.GetFiles(saveDir, "*.data");

        return saveFiles;
    }

    public void RefreshSaveList()
    {
        string[] saveFiles = GetSavePaths();

        foreach (string filePath in saveFiles)
        {
            // Get only the filename (without directory and extension)
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            savedGamesDropdown.options.Add(new TMP_Dropdown.OptionData(fileName));
        }

        // Refresh the dropdown visually
        savedGamesDropdown.RefreshShownValue();
    }

    public void ChooseSelectedPath()
    {
        string Choosen = ""; // you were the choosen one anakin

        Choosen = savedGamesDropdown.options[savedGamesDropdown.value].text;

        Choosen = SerilizationManager.GetSavePath(Choosen);

        FileSelectionManager.SelectedFilePath = Choosen;
    }

    public void DeleteSaveList()
    {

        string[] SavePaths = GetSavePaths();

        foreach (string filePath in SavePaths)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);

            File.Delete(filePath);
        }
    }
}
