using UnityEngine;
using UnityEngine.SceneManagement;
public class ScaneChanger : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
