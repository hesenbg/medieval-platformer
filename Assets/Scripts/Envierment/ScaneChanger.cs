using UnityEngine;
using UnityEngine.SceneManagement;
public class ScaneChanger : MonoBehaviour
{
    [SerializeField] int MainSceneIndex;
    [SerializeField] int SettingsSceneIndex;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadSceneAsync(MainSceneIndex);
        }
    }

    public void Play()
    {
        SceneManager.LoadSceneAsync(MainSceneIndex);
    }

    public void Settings()
    {
        SceneManager.LoadSceneAsync(SettingsSceneIndex);
    }

    public void ShotDownGame()
    {
        Application.Quit();        
    }


    public void PouseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
