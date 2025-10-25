using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;
public class CameraLogic : MonoBehaviour
{
    void Update()
    {
        transform.localScale = Vector3.one;
    }
    [SerializeField] Vector2 OBPosition;
    [SerializeField] Vector2 OBsize;
    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;

        // Set gizmo color

        // Draw the overlap box at the same position and size
        Gizmos.DrawWireCube(OBPosition, OBsize);
    }

}
