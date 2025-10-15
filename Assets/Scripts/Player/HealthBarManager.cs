using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Player player;
    Melee melle;
    private void Start()
    {
        melle = GameObject.FindGameObjectWithTag("Player").GetComponent<Melee>();
        slider.maxValue = player.MaxHealth; 
    }
    public void UpdateHealthBar()
    {
        slider.value = player.CurrentHealth;
    }
    
    private void LateUpdate()
    {
        UpdateHealthBar();
        Debug.Log(slider.value);
    }
}
