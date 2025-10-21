using UnityEngine;

public class UIswitcher : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] Canvas PlayerUI;
    [SerializeField] Canvas MainMenuUI;
    [SerializeField] Canvas SettingsMenuUI;
    [SerializeField] Canvas BackGroundImage;

    private enum UIsituation { Player, Settings, MainMenu }
    [SerializeField] UIsituation situation;

    private void Start()
    {
        EnablePlayerUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            situation = UIsituation.MainMenu;
        }


        // Optional: Handle state changes here if needed
        switch (situation)
        {
            case UIsituation.Player:
                EnablePlayerUI();
                break;
            case UIsituation.Settings:
                EnableSettingsUI();
                break;
            case UIsituation.MainMenu:
                EnableMainMenuUI();
                break;
        }
    }

    // === Public UI Switch Functions ===

    public void EnablePlayerUI()
    {
        situation = UIsituation.Player;
        PlayerUI.enabled = true;
        MainMenuUI.enabled = false;
        SettingsMenuUI.enabled = false;
        BackGroundImage.enabled = false;
    }

    public void EnableSettingsUI()
    {
        situation = UIsituation.Settings;
        PlayerUI.enabled = false;
        MainMenuUI.enabled = false;
        SettingsMenuUI.enabled = true;
        //BackGroundImage.enabled = true;
    }

    public void EnableMainMenuUI()
    {
        situation = UIsituation.MainMenu;
        PlayerUI.enabled = false;
        MainMenuUI.enabled = true;
        SettingsMenuUI.enabled = false;
        //BackGroundImage.enabled = true;
    }

    public void ShotDown()
    {
        Application.Quit();
    }
}
