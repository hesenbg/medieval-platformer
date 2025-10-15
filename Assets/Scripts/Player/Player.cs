using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // === Components ===
    Rigidbody2D rb;
    TrailRenderer tr;
    Animator animator;
    SpriteRenderer sp;
    [SerializeField] KillZone kilZone;
    [SerializeField] SoundManager soundManager;
    // === Movement Variables ===
    [SerializeField] float speed;
    [SerializeField] float JumpForce;
    [SerializeField] float DashValue;
    [SerializeField] float DashDuration;
    [SerializeField] float AirSlowerValue;
    public float Direction = 1;
    public float RunFloat; // parameter for blend tree: 1 when running, 0 when idle

    // === Jump Variables ===
    private float MaxMultipleJumpCount = 2;
    float CurrentMultipleJumpCount;
    [SerializeField] public bool isAir = false;
    [SerializeField] bool isJumping = false;
    [SerializeField] bool isFalling = false;

    // === Dash Variables ===
    bool IsDashing = false;
    bool IsDashAble = true;
    float DashCooldown;
    float DashCooldownValue = 2f;

    // === Fight Variables ===
    public float CurrentHealth;
    public float MaxHealth;

    // === Unity Events ===
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        animator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        CurrentMultipleJumpCount = MaxMultipleJumpCount;
        CurrentHealth = MaxHealth;
    }

    void Update()
    {
        Flip();
        Movement();
        JumpSituations();
        DashSituations();
        SettingAnimationParameters();
        CheckPlayerDie();
        SpeedSlower();
    }

    [SerializeField] HealthBarManager healthBarManager;
    [SerializeField] float DamageGuard = 25; // damage taken but enemy guard

    public bool IsOnGround= false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // small trigger in player's feet detects if player on ground
        if (collision.gameObject.layer == 7)
        {
            IsOnGround = true;
            isFalling = false;
            CurrentMultipleJumpCount = MaxMultipleJumpCount;
            isAir = false;
        }
        // projectile hits player
        if(collision.gameObject.layer == 8)
        {
            GetDamage(DamageGuard);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            IsOnGround = false;
        }
    }

    // 
    public void GetDamage(float Damage)
    {     
        CurrentHealth -= Damage;
        soundManager.PlayHurt();
    }

    // setting player's movement paramaters
    void JumpSituations()
    {
        // Air conditions
        if (rb.linearVelocityY != 0)
        {
            if (rb.linearVelocityY < -0.5f)
            {
                isJumping = false;
                isFalling = true;
            }
            else if (rb.linearVelocityY > 0.5f)
            {
                isFalling = false;
                isJumping = true;
            }
        }
        else
        {
            isJumping = false;
            isFalling = false;
        }
    }
    void DashSituations()
    {
        // Dash cooldown
        if (DashCooldown >= 0 && !IsDashAble)
        {
            DashCooldown -= Time.deltaTime;
        }
        else
        {
            IsDashAble = true;
        }
    }

    void SettingAnimationParameters()
    {
        // Animation decider
        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsAir", isAir);
        animator.SetBool("IsFalling", isFalling);
        animator.SetFloat("SpeedX", RunFloat);
    }
    void Flip()
    {
        // Flipping
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * Direction;
        transform.localScale = scale;
    }
    void Movement()
    {
        HandleJump();
        HandleHorizontalMovement();
        HandleDash();
    }
    // cant jump when dashing
    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CurrentMultipleJumpCount > 0 && !IsDashing)
        {
            isAir = true;
            rb.linearVelocityX = 0;
            rb.linearVelocityY = JumpForce;
            CurrentMultipleJumpCount--;
        }
    }
    // cant move while dashing
    void HandleHorizontalMovement()
    {
        if (IsDashing) return;

        if (Input.GetKey(KeyCode.A))
        {
            RunFloat = 1;
            Direction = -1;
            rb.linearVelocityX = -speed*SpeedSlower();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RunFloat = 1;
            Direction = 1;
            rb.linearVelocityX = speed*SpeedSlower();
        }
        else if (!isAir) // player stops if not pressing A/D except in midair
        {
            RunFloat = 0;
            rb.linearVelocityX = 0;
        }
    }

    void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.Q) && IsDashAble)
        {
            StartCoroutine(Dash());
        }
    }

    float SpeedSlower()
    {
        if (isAir)
        {
            return AirSlowerValue;
        }
        else
        {
            return 1f;
        }
    }

    IEnumerator Dash()
    {
        float originalGravity = rb.gravityScale;
        float originalSpeed = rb.linearVelocityX;

        // Dash start
        rb.linearVelocityY = 0;
        rb.linearVelocityX = DashValue * Direction;
        rb.gravityScale = 0;
        tr.emitting = true;
        IsDashing = true;

        yield return new WaitForSeconds(DashDuration);

        // Dash end
        rb.linearVelocityX = originalSpeed;
        rb.gravityScale = originalGravity;
        tr.emitting = false;
        IsDashing = false;
        IsDashAble = false;
        DashCooldown = DashCooldownValue;
    }
    // check if player died
    void CheckPlayerDie()
    {
        if (CurrentHealth <= 0)
        {
            kilZone.SetPlayerCheckPoint();
        }
    }
}
