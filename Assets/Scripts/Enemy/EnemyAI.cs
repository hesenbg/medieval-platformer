using Unity.Mathematics;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // === External References ===
    private GameObject player;
    [HideInInspector] public Rigidbody2D rb;
    private GuardSight guardSight;
    [HideInInspector] public GroundCheking rightChecker;
    public EnemyAnimationManager AnimationManager;

    EnemyCombat combat;

    // === Health & Combat ===
    private float damagePushVelocity = 1f;
    private float stunDuration = 1.5f; 
    float CurDirection;

    // === Movement ===
    [SerializeField] float idleSpeed = 2.5f;
    [SerializeField] float chaseSpeed = 4f;
    [SerializeField] float moveDirection = 1f;
    [SerializeField] float StunDuration;
    float StunDurationValue =0;

    // === State Management ===
    public enum State { Wander, Fighting, Stunned, Chasing }
    public State currentState = State.Wander;

    public enum EnemyType { ranged, melee }
    public EnemyType CurrentEnemyType;

    // === Flags ===
    bool CanAttackAble = false;

    void Start()
    {
        player = GameObject.Find("Player").gameObject;
        rb = GetComponent<Rigidbody2D>();
        guardSight = gameObject.GetComponentInChildren<GuardSight>();

        rightChecker = gameObject.GetComponentInChildren<GroundCheking>();
        combat = GetComponent<EnemyCombat>();

        CurDirection = moveDirection;
    }

    private void Update()
    {
        UpdateAI();
        Flip();
    }

    public void UpdateAI()
    {
        HandleTransitions();
        ExecuteCurrentState();
    }

    // === Transitions ===
    private void HandleTransitions()
    {
        if(currentState != State.Stunned)
        {
            if (guardSight.IsPlayerOnSight && !guardSight.IsPlayerOnRange)
            {
                currentState = State.Chasing;
            }
            else if (guardSight.IsPlayerOnSight && guardSight.IsPlayerOnRange)
            {
                currentState = State.Fighting;
            }
            else if (!guardSight.IsPlayerOnSight)
            {
                currentState = State.Wander;
            }
        }
    }

    // === State Logic ===
    private void ExecuteCurrentState()
    {
        switch (currentState)
        {
            case State.Wander:
                Wander();
                break;
            case State.Fighting:
                Fight();
                break;
            case State.Stunned:
                Stun();
                break;
            case State.Chasing:
                Chase();
                break;
        }
    }

    void Stun()
    {
        // animation plays when player hits enemy

        // apply logic
        rb.linearVelocity = Vector3.zero;
    }

    void Chase()
    {
        if (player == null) return;
        moveDirection = math.sign(player.transform.position.x - transform.position.x);
        rb.linearVelocityX = chaseSpeed * moveDirection;
    }

    float PrevDirection;

    private void Wander()
    {
        PrevDirection = moveDirection;  

        // flip direction when edges are reached
        if (!rightChecker.IsPressing)
        {
            moveDirection *= -1;
            
        }

        rb.linearVelocityX = idleSpeed * PrevDirection;
    }

    void Flip()
    {
        if (CurDirection != moveDirection)
        {
            CurDirection  = moveDirection;
            transform.Rotate(0,180, 0);
        }
    }

    private void Fight()
    {
        rb.linearVelocity = Vector2.zero;
        combat.Attack();

        //  combat idle animation
        AnimationManager.FightAnimation();
    }


    // === Visual Direction ===
    public float GetDirection() => moveDirection;
}
