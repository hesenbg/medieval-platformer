using UnityEngine;

public class GuardSight : MonoBehaviour
{ 
    public EnemyAI enemy;
    [SerializeField] LayerMask GuardSightLM ;
    public float GuardSightDistance;
    public bool IsPlayerOnSight = false;
    public bool IsPlayerOnRange = false;

    [SerializeField] float MeleeRange;
    [SerializeField] float ArcherRange;

    float CurrentRange;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        GuardSightLM= LayerMask.GetMask("Player");
    }
    void Update()
    {
        switch (enemy.CurrentEnemyType) {
            case EnemyAI.EnemyType.ranged:
                CurrentRange = ArcherRange;
                break;
            case EnemyAI.EnemyType.melee:
                CurrentRange = MeleeRange;
                break;
        }

        IsPlayerOnSight = CheckIsPlayerOnSight();
        IsPlayerOnRange = CheckIsPlayerOnRange(CurrentRange);

    }
    public bool CheckIsPlayerOnSight()
    {
        return  Physics2D.Raycast(transform.position, transform.right,GuardSightDistance,GuardSightLM);
    }

    public bool CheckIsPlayerOnRange(float Range)
    {
        if (IsPlayerOnSight && Physics2D.Raycast(transform.position, transform.right, CurrentRange, GuardSightLM))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;

        // Draw the overlap box at the same position and size
        Gizmos.color =  UnityEngine.Color.green;

        Gizmos.DrawLine(transform.position, transform.position+ transform.right.normalized*GuardSightDistance);
    }
}