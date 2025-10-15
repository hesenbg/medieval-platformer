using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    GameObject Target;
    float Speed = 6f;
    float damage = 5f;
    public float Direction= 1f;
    float FadeDuration = 2f;
    void Update()
    {
        Move();
        DestroyAfter();
        transform.localScale = new Vector3(0.2f, 0.3f, 0.2f);
    }
    private void Awake()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        Direction = Mathf.Sign(Target.transform.position.x - transform.position.x);
    }

    void Move()
    {
        rb.linearDamping = Speed;
        rb.linearVelocityX = Speed*Direction;
    }
    void DestroyAfter()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")){
            Destroy(gameObject);          
        }
    }
}
