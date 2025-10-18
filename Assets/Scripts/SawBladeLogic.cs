using UnityEngine;

public class SawBladeLogic : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float Duration;
    float DurationValue;
    [SerializeField] float Speed;
    [SerializeField] float WaitDelay;
    float WaitDelayValue;

    [Header("Damage Settings")]
    [SerializeField] float BladeDamage;
    [SerializeField] float Radius;
    [SerializeField] LayerMask PlayerMask;
    [SerializeField] float PushForce;

    [SerializeField] enum BladeMoveAxis { X, Y }
    [SerializeField] BladeMoveAxis MoveAxis;

    Rigidbody2D rb;
    Player player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        DamagePlayer();
    }

    void DamagePlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, Radius, PlayerMask);
        if (hit == null) return;

        player = hit.GetComponent<Player>();
        if (player != null)
            player.GetBladeDamager(BladeDamage,transform.position,PushForce);
    }

    void Move()
    {
        if (MoveAxis == BladeMoveAxis.X)
        {
            if (DurationValue < Duration)
            {
                DurationValue += Time.deltaTime;
                rb.linearVelocityX = Speed;
            }
            else
            {
                if (WaitDelay > WaitDelayValue)
                {
                    WaitDelayValue += Time.deltaTime;
                    rb.linearVelocityX = 0;
                }
                else
                {
                    DurationValue = 0;
                    Speed = -Speed;
                    WaitDelayValue = 0;
                }
            }
        }
        else if (MoveAxis == BladeMoveAxis.Y)
        {
            if (DurationValue < Duration)
            {
                DurationValue += Time.deltaTime;
                rb.linearVelocityY = Speed;
            }
            else
            {
                if (WaitDelay > WaitDelayValue)
                {
                    WaitDelayValue += Time.deltaTime;
                    rb.linearVelocityY = 0;
                }
                else
                {
                    DurationValue = 0;
                    Speed = -Speed;
                    WaitDelayValue = 0;
                }
            }
        }
    }

    // === GIZMOS ===
    private void OnDrawGizmos()
    {
        // Draw Damage Radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);

        // Draw movement direction
        Gizmos.color = Color.yellow;
        Vector3 dir = Vector3.zero;

        if (MoveAxis == BladeMoveAxis.X)
            dir = Vector3.right * Mathf.Sign(Speed);
        else if (MoveAxis == BladeMoveAxis.Y)
            dir = Vector3.up * Mathf.Sign(Speed);

        Gizmos.DrawLine(transform.position, transform.position + dir * 2f);
        Gizmos.DrawSphere(transform.position + dir * 2f, 0.1f);
    }
}
