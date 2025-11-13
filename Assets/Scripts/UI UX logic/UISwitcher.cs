using UnityEngine;

public class UISwitcher : MonoBehaviour
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

}
