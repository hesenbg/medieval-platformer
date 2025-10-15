using UnityEngine;
public class KIlZone : MonoBehaviour
{
    Player player;
    [SerializeField] CheckPoint CheckPoint;
    [SerializeField] HealthBarManager healthBarManager;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void SetPlayerCheckPoint()
    {
        player.transform.position = CheckPoint.transform.position;
        player.CurrentHealth = player.MaxHealth;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer==6)
        {
            if(player != null)
                player.transform.position = CheckPoint.transform.position;
                player.CurrentHealth = player.MaxHealth;
        }
    }
}