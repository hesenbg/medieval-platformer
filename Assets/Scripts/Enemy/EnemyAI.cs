using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyAI : MonoBehaviour
{
    // === External References ===
    private GameObject player;
    [HideInInspector] public Rigidbody2D rb;
    private GuardSight guardSight;
    [HideInInspector] public GroundCheking rightChecker;
    [HideInInspector] public EnemyAnimationManager AnimationManager;

    [SerializeField] EnemyCombat combat;

    // === Health & Combat ===
    private float damagePushVelocity = 1f;
    private float stunDuration = 1.5f; 

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
        guardSight = GameObject.Find("GuardSight").GetComponent<GuardSight>();

        rightChecker = GameObject.Find("RGroundCheking").GetComponent<GroundCheking>();
        combat = GetComponent<EnemyCombat>();
    }

    private void Update()
    {
        UpdateAI();
    }

    public void UpdateAI()
    {
        HandleTransitions();
        ExecuteCurrentState();
    }

    // === Transitions ===
    private void HandleTransitions()
    {
        bool playerVisible = guardSight != null && guardSight.IsPlayerOnSight;

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
        // play animation

        // apply logic
        rb.linearVelocity = Vector3.zero;
    }

    void Chase()
    {
        if (player == null) return;
        moveDirection = math.sign(player.transform.position.x - rb.transform.position.x);
        rb.linearVelocityX = chaseSpeed * moveDirection;
    }

    private void Wander()
    {
        // flip direction when edges are reached
        if (!rightChecker.IsPressing)
        {
            moveDirection *= -1;
            transform.Rotate(0, 180f, 0);
        }

        rb.linearVelocityX = idleSpeed * moveDirection;
    }

    private void Fight()
    {
        rb.linearVelocity = Vector2.zero;
        combat.Attack();
    }


    // === Visual Direction ===
    public float GetDirection() => moveDirection;
}
