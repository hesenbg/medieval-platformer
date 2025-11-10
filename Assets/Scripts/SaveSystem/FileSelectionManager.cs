using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class FileSelectionManager : MonoBehaviour
{
    public static FileSelectionManager Instance { get; set; }

    // Holds the currently selected file path (accessible from any scene)
    public string SelectedFilePath { get; set; }

    // Optional: default filename if user hasn't picked one yet
    [SerializeField] private string defaultFileName = "savegame.dat";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (string.IsNullOrWhiteSpace(SelectedFilePath))
        {
            var fallbackDir = Application.persistentDataPath;
            SelectedFilePath = Path.Combine(fallbackDir, defaultFileName);
        }

    }



}