using UnityEngine;

public class MovingSawBlade : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Movement Settings")]
    [SerializeField] float rightMass = 1f;
    [SerializeField] float upMass = 0f;
    [SerializeField] float amplitude = 5f;     // Max speed or force
    [SerializeField] float frequency = 1f;     // Oscillation speed (cycles per second)

    [Header("Debug")]
    [SerializeField] Vector3 resultVector;
    [SerializeField] float timeCounter;

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
        float sine = Mathf.Sin(timeCounter * frequency * Mathf.PI * 2);  // full sine wave cycle

        // Velocity changes smoothly between +amplitude and -amplitude
        rb.linearVelocity = resultVector * sine * amplitude;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + resultVector.normalized * 3f);
    }
}
