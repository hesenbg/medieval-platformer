using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonLogics : MonoBehaviour
{
    [Header("UIs")]
    [SerializeField] Canvas SettingsMenu;

    [SerializeField] Canvas NewGameSave;

    [SerializeField] Canvas MainMenu;

    [SerializeField] Canvas SavedGamesUI;

    private void Update()
    {
        //Debug.Log(FileSelectionManager.Instance.SelectedFilePath);
    }

    public void AddSaveOption(string SaveName)
    {
        Debug.Log(SaveName);
        SerilizationManager.CreateSaveFile(SaveName);
        BackMainMenu();
    }

    public void LoadSavedGamesUI()
    {
        SavedGamesUI.enabled = true;

        MainMenu.enabled = false;
    }

    public void LoadSettingsMenu()
    {
        MainMenu.enabled = false;

        SettingsMenu.enabled = true;
    }

    public void LoadGameSaveMenu()
    {
        MainMenu.enabled = false;

        SettingsMenu.enabled = false;

        NewGameSave.enabled = true;
    }
    public void BackMainMenuFromSave()
    {
        MainMenu.enabled = true;

        SettingsMenu.enabled = false;

        NewGameSave.enabled = false;
    }

    public void BackMainMenu()
    {
        MainMenu.enabled = true;

        SettingsMenu.enabled = false;

        NewGameSave.enabled = false;
    }

    public void PrevButton(Canvas Current)
    {
        Current.enabled = false;

        MainMenu.enabled = true;
    }


    public void ChooseSelectedPath(TMP_Dropdown Paths)
    {
        string Choosen = ""; // you were the choosen one anakin

        Choosen = Paths.options[Paths.value].text;

        Choosen = SerilizationManager.GetSavePath(Choosen);

        FileSelectionManager.SelectedFilePath = Choosen;

        Debug.Log(FileSelectionManager.SelectedFilePath);
    }

    public void DeleteAllSaves()
    {
        string NewPath="";

        string[] filePaths = Directory.GetFiles(Application.persistentDataPath);
        foreach (string fs in filePaths)
            NewPath = Path.Combine(Application.persistentDataPath, "saves");
            File.Delete(NewPath);
    }

    public void LoadGame()
    {
        // check if choosen path has data in it 
        object Data = SerilizationManager.Load(FileSelectionManager.SelectedFilePath
            , true);
        
        if(Data == null)
        {
            SceneManager.LoadSceneAsync(1);
        }
        else
        {
            FileSelectionManager.PlayerData = (SavePlayerData)Data;

            SceneManager.LoadSceneAsync(FileSelectionManager.PlayerData.Level);
        }
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
