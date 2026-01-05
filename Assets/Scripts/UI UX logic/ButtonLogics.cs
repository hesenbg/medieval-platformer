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
        SerilizationManager.LoadSelectedGame();
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
