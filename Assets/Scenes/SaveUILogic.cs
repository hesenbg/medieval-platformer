using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveUILogic : MonoBehaviour
{

    [SerializeField] TMP_InputField SaveInputField;

    [SerializeField] float HeightDifference;

    float HeightValue;

    [SerializeField] GameObject ContentWindow;

    Vector3 pos;
   
    private void Update()
    {
        Debug.Log(SaveInputField.transform.localPosition);
    }

    private void Start()
    {
        HeightValue = HeightDifference;
    }

    public void AddSaveField()
    {
        pos = SaveInputField.transform.position; 
        pos = new Vector3(pos.x,pos.y-=HeightValue,pos.z);
        HeightValue += HeightDifference;

        Instantiate(SaveInputField,pos,
            SaveInputField.transform.rotation,
            ContentWindow.transform);
    }
}
