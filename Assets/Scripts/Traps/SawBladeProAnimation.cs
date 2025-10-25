using UnityEngine;

public class SawBladeProAnimation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 360f; 

    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
