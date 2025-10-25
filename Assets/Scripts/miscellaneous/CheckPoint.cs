using UnityEngine;
public class CheckPoint : MonoBehaviour
{
    Player player;

    [SerializeField] float CheckTime = 5f;
    float CheckCurrentTime = 0;
    [SerializeField] float CheckDistance = 6f;
    void Start()
    {
        player =  GameObject.FindWithTag("Player").GetComponent<Player>();
        transform.position = player.transform.position;
    }
    void Update()
    {        
        if(CheckTime > CheckCurrentTime)
        {
            CheckCurrentTime += Time.deltaTime;
        }
        else
        {
            if(Mathf.Abs( player.transform.position.x-transform.position.x) > CheckDistance  && player.IsOnGround)
            {
                transform.position = player.transform.position;
            }
        }
    }

    public void RespawnPlayer()
    {
        player.transform.position = transform.position;
    }
}