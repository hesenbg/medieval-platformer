using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Guard : MonoBehaviour
{
    // === References ===
    public GameObject player;
    public Melee melee;
    public GuardSight guardSight;
    public Rigidbody2D rb;
    [SerializeField] GroundCheking Rgc;
    [SerializeField] GroundCheking Lgc;
    [SerializeField] EnemyAnimationManager Decider;
    [SerializeField] SoundManager SM;

    // === Layers ===
    private LayerMask PlayerLM;  // for detecting the player
    [SerializeField] private LayerMask GroundLM;

    // === Ground Checking ===
    private Vector2 GroundDirection;
    [SerializeField] private bool IsPressingGround = true;

    // === Fight Parameters ===
    private float GuardMaxHealth = 100f;
    [SerializeField] private float GuardCurrentHealth;
    [SerializeField] private float DamagePushVelocity = 1f;
    [SerializeField] float FightDistance = 5f;  // distance treshold between guard and player to start a fight
    [SerializeField] Vector2 OBPosition;
    [SerializeField] float StunDuration = 1.5f;

    // === Movement ===
    [SerializeField] private float IdleSpeed = 2.5f;
    [SerializeField] private float ChaseSpeed = 4f;
    float moveDirection; // 1 = right, -1 = left
    float WonderDirection;

    // === Movement States ===
    public bool IsPressingLeft = true;
    public bool IsPressingRight = true;
    public bool IsWandering = true;
    public bool IsFighting = false;
    private bool IsPlayerAround = false;
    public bool IsRunning = true;

    // === Guard AI States ===
    public enum GuardState { Wander, Fighting, Stunned }
    public GuardState currentState;
    public float AttackDuration = 3f;
    public float CurrentAttackDuration = 0;

    // === Unity Events ===
    void Start()
    {
        SM= GameObject.FindGameObjectWithTag("SM").GetComponent<SoundManager>();

        OBPosition = transform.position;

        moveDirection = 1f;

        // ground check init
        GroundLM = LayerMask.GetMask("Ground");
        GroundDirection = new Vector2(0, -1);

        // health
        GuardCurrentHealth = GuardMaxHealth;

        // guard AI default states
        currentState = GuardState.Wander;
        PlayerLM = LayerMask.GetMask("Player");

        rb.linearDamping = IdleSpeed;
    }
  

    void Update()
    {
        CheckGotDamage();
    }

    private void FixedUpdate()
    {
        GuardAi();
        UpdateDirection();
        IsPlayerAround = guardSight.IsPlayerOnSight;
        Die();
    }

    void LateUpdate()
    {
        GuardSituations();
    }

    // === AI & Logic ===
    public void GuardAi()
    {
        if (IsWandering)
        {
            currentState = GuardState.Wander;
        }
        if (IsFighting)
        {
            currentState = GuardState.Fighting;
        }

        switch (currentState)
        {
            case GuardState.Wander:
                Wonder();
                break;
            case GuardState.Fighting:
                ChasePlayer();
                break;
        }
    }

    void GuardSituations()
    {
        // Wonder & triggered
        if (guardSight.IsPlayerOnSight || IsPlayerAround)
        {
            IsFighting = true;
            IsWandering = false;
        }
        else
        {
            IsWandering = true;
            IsFighting = false;
        }

        // ground pressing
        if (IsPressingLeft || IsPressingRight)
        {
            IsPressingGround = true;
        }
        else if (!IsPressingLeft && !IsPressingRight)
        {
            IsPressingGround = false;
        }
    }

    void CheckGotDamage()
    {
        if (melee.GotDamage)
        {
            StartCoroutine(GotDamage());
        }
    }

    public IEnumerator GotDamage()
    {
        // play animation
        Decider.StunAnimation(true);
        
        // logic
        rb.linearVelocityX = DamagePushVelocity * player.transform.localScale.x;
        GuardCurrentHealth -= melee.DamageGiven;
        currentState = GuardState.Stunned;

        yield return new WaitForSeconds(1f);

        // stop animation
        currentState = GuardState.Fighting;
        Decider.StunAnimation(false);
    }

    void UpdateDirection()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * moveDirection;
        transform.localScale = scale;
    }
    void Wonder()
    {
        WonderDirection = moveDirection;

        if (!Rgc.IsPressing && Lgc.IsPressing)
        {
            moveDirection *= -1;
        }
        if (Rgc.IsPressing && !Lgc.IsPressing)
        {
            moveDirection *= 1;
        }

        WonderDirection = moveDirection;
        rb.linearVelocityX = IdleSpeed * WonderDirection;
    }

    void ChasePlayer()
    {
        moveDirection = math.sign(player.transform.position.x - transform.position.x);
    }

    void Die()
    {
        if (GuardCurrentHealth <= 0 && currentState == GuardState.Fighting)
        {
            Destroy(gameObject);
        }
    }
}
