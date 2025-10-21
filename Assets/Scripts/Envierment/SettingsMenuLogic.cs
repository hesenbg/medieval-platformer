using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenuLogic : MonoBehaviour
{
    [SerializeField] Toggle FullScreen;

    [SerializeField] Slider Sound;

    [SerializeField] AudioMixer mainMixer;

    private void Update()
    {
        ToggleFullscreen();
        SetMasterVolume(Sound.value);
    }
    public void SetMasterVolume(float volume)
    {
        mainMixer.SetFloat("VFXVolume", Mathf.Log10(volume) * 20);
    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = FullScreen.isOn;
    }

    public void BackMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
