using UnityEngine;
using UnityEngine.SceneManagement;

public class UIswitcher : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] Canvas PlayerUI;
    [SerializeField] Canvas PauseMenu;

    [SerializeField] int MainMenuIndex;

    private enum UIsituation { Player, Settings, MainMenu }
    [SerializeField] UIsituation situation;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.enabled = true;
        }
    }

    public void SaveGame()
    {

    }

    public void LoadGame()
    {

    }

    public void CancelPause()
    {
        PauseMenu.enabled = false;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync(MainMenuIndex);
    }


    public void ShotDown()
    {
        Application.Quit();
    }
}
