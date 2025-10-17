using UnityEngine;
using UnityEngine.SceneManagement;
public class ScaneChanger : MonoBehaviour
{
    [SerializeField] int MainSceneIndex;
    [SerializeField] int SettingsSceneIndex;

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
}
