using System.Collections;
using Unity.Mathematics;
using UnityEngine;


public class EnemyAI : MonoBehaviour
{
    // === External References ===
    private GameObject player;
    [HideInInspector] public Rigidbody2D rb;
    private GuardSight guardSight;
    [HideInInspector] public GroundCheking leftChecker;
    [HideInInspector] public GroundCheking rightChecker;
    [HideInInspector] public EnemyAnimationManager AnimationManager;

    // === Health & Combat ===
    private float damagePushVelocity = 1f;
    private float stunDuration = 1.5f;

    // === Movement ===
    [SerializeField] float idleSpeed = 2.5f;
    [SerializeField] float chaseSpeed = 4f;
    [SerializeField] float moveDirection = 1f;

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
        guardSight = GetComponent<GuardSight>();

        leftChecker = GameObject.Find("LGroundCheking").GetComponent<GroundCheking>();
        rightChecker = GameObject.Find("RGroundCheking").GetComponent<GroundCheking>();

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

        if (playerVisible)
        {
            currentState = State.Fighting;
        }
        else
        {
            currentState = State.Wander;
            CanAttackAble = false;
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

    }

    void Chase()
    {

    }

    private void Wander()
    {
        // flip direction when edges are reached
        if (!rightChecker.IsPressing && leftChecker.IsPressing)
        {
            moveDirection *= -1;
            RotateEnemy();
        }
        else if (rightChecker.IsPressing && !leftChecker.IsPressing)
        {
            moveDirection *= 1;
            RotateEnemy();
        }

        rb.linearVelocity = idleSpeed * transform.right.normalized;
    }

    private void Fight()
    {
        if (player == null) return;
        if (CurrentEnemyType == EnemyType.melee)
        {
            moveDirection = math.sign(player.transform.position.x - rb.transform.position.x);
            rb.linearVelocityX = chaseSpeed * moveDirection;
        }
        else if (CurrentEnemyType == EnemyType.ranged)
        {
            CanAttackAble = true;
        }
    }

    void RotateEnemy()
    {
        float NewY = 0;
        NewY += 180;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x,
            NewY,
            transform.eulerAngles.z);
    }

    // === Visual Direction ===
    public float GetDirection() => moveDirection;
}
