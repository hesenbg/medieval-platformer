using TMPro;
using UnityEngine;
using UnityEngine.UI; // or TMPro

public class SavePathInputBinder : MonoBehaviour
{
    public void OnUserEnteredPath(string path)
    {
        if (FileSelectionManager.Instance == null) return;
        
        //FileSelectionManager.Instance.SetSelectedFilePath(path);
    }
}