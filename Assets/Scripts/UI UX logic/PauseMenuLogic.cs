using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenuLogic : MonoBehaviour
{
    [SerializeField] Canvas PauseMenu;

    [SerializeField] KeyCode PauseKey;

    private void Update()
    {
        if (Input.GetKeyDown(PauseKey))
        {
            Time.timeScale = 0;

            PauseMenu.enabled = true;
        }
    }

    public void BackGame()
    {
        PauseMenu.enabled = false;
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }
}
