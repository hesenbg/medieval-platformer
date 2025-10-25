using UnityEngine;

public class MovingSawBlade : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Movement Settings")]
    [SerializeField] float rightMass = 1f;
    [SerializeField] float upMass = 0f;
    [SerializeField] float amplitude = 5f;     // Max speed or force
    [SerializeField] float frequency = 1f;     // Oscillation speed (cycles per second)

    [Header("Damage")]
    [SerializeField] float damage = 10f;
    [SerializeField] float pushForce = 5f;

    [Header("Debug")]
    [SerializeField] Vector3 resultVector;
    [SerializeField] float timeCounter;

    private Player player;
    private bool isPlayerTouching = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Direction of movement
        resultVector = (transform.right * rightMass) + (transform.up * upMass);
        resultVector.Normalize();

        // Smooth oscillation using sine wave
        timeCounter += Time.fixedDeltaTime;
        float sine = Mathf.Sin(timeCounter * frequency * Mathf.PI * 2f);  // full sine wave cycle

        // Velocity changes smoothly between +amplitude and -amplitude
        rb.linearVelocity = resultVector * sine * amplitude;
    }

    private void Update()
    {
        DamagePlayer();
    }

    // --- Trigger-based detection ---
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            player = other.GetComponent<Player>();
            isPlayerTouching = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            isPlayerTouching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            isPlayerTouching = false;
        }
    }

    private void DamagePlayer()
    {
        if (isPlayerTouching && player != null)
        {
            player.GetBladeDamage(damage, transform.position, pushForce);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + resultVector.normalized * 3f);
    }
}
