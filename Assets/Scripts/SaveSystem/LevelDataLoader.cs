using UnityEngine;

public class LevelDataLoader : MonoBehaviour
{
    [SerializeField] Player player;

    private void Awake()
    {
        SavePlayerData data = (SavePlayerData)SerilizationManager.Load(FileSelectionManager.GetCurrentPath(),true);

        if(data != null)
            player.Load(data);
    }

    private void Update()
    {
        Debug.Log(FileSelectionManager.GetCurrentPath());
    }
}
