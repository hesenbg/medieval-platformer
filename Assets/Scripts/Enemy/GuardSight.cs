using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class GuardSight : MonoBehaviour
{ 
    public Guard guard;
    LayerMask GuardSightLM ;
    public float GuardSightDistance= 10f;
    public bool IsPlayerOnSight = false;
    [SerializeField] Vector2 OBsize = new Vector2(3, 3);  // overlapbox vector for detecting player around
    void Start()
    {
        GuardSightLM= LayerMask.GetMask("Player");
    }
    void Update()
    {
        IsPlayerOnSight = CheckIsPlayerAround(transform.position, OBsize);
    }
    bool CheckIsPlayerAround(Vector2 origin, Vector2 size)
    {
        return Physics2D.OverlapBox(transform.position, size, 0, GuardSightLM);
    }
    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;

        // Draw the overlap box at the same position and size
        Gizmos.DrawWireCube(transform.position, OBsize);
    }
}