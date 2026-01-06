using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    // deleting and refreshing the paths
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

    // refreshes when new pth added to the dropdown
    public void RefreshSaveList()
    {
        string[] saveFiles = GetSavePaths();
        if (saveFiles == null || saveFiles.Length == 0) return;

        savedGamesDropdown.ClearOptions();
        List<string> options = new List<string>();

        foreach (string filePath in saveFiles)
        {
            options.Add(Path.GetFileNameWithoutExtension(filePath));
        }

        savedGamesDropdown.AddOptions(options);
        savedGamesDropdown.RefreshShownValue();

        // This ensures the first item is selected in logic as well as UI
        if (options.Count > 0)
        {
            savedGamesDropdown.value = 0;
            ChooseSelectedPath(0);
        }
    }

    // updates the choosen path in dropdown
    public void ChooseSelectedPath(int Value)
    {
        string Choosen = ""; // you were the choosen one anakin

        Choosen = savedGamesDropdown.options[Value].text;

        Choosen = SerilizationManager.GetSavePath(Choosen);

        FileSelectionManager.SelectedFilePath = Choosen;
    }

    // used in dropdown 
    public void DeleteSaveList()
    {

        string[] SavePaths = GetSavePaths();

        foreach (string filePath in SavePaths)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);

            File.Delete(filePath);
        }
    }

    // if data is null, loads up the default 
    public void LoadGame()
    {
        SceneManager.LoadSceneAsync(1);
        return;

        // check if choosen path has data in it 
        object Data = SerilizationManager.Load(FileSelectionManager.SelectedFilePath
            , true);

        if (Data == null)
        {
        }
        else
        {
            SceneManager.LoadSceneAsync(1);
        }
    }
}
