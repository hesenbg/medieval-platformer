using UnityEngine;

public class Melee : MonoBehaviour
{
    bool IsAttacking = false;
    Animator animator;
    [SerializeField] float Attack1Duration;
    float Attack1DurationValue;
    public  float PlayerDamage = 50;
    public  float DamageGiven=0;
    public bool GotDamage = false;
    SoundManager Soundmanager;

    [SerializeField] float AttackRange = 5f;
    LayerMask EnemyLayer;
    public Transform AttackSpace;

    private void Start()
    {
        Soundmanager = GameObject.FindGameObjectWithTag("SM").GetComponent<SoundManager>();

        Attack1DurationValue = 0;
        animator = GetComponent<Animator>();
        EnemyLayer = LayerMask.GetMask("Hostile");
    }
    void Update()
    {
        // reset every frame when player doesnt attack
        GotDamage = false;

        AttackTimer();

        GetAtackInput();
    }

    float AttackFloat=1;
    float MaxAttackFloatValue = 3;
    void GetAtackInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && !IsAttacking)
        {
            // play animation
            animator.SetFloat("AttackFloat",AttackFloat);
            // do attack logic
            Attack();
            // play sound
            Soundmanager.PlaySlash();
            // set variables
            IsAttacking = true;

            UpdateAttackFloat();
            
        }
    }
    void UpdateAttackFloat()
    {
        if (AttackFloat < MaxAttackFloatValue)
        {
            AttackFloat += 1;
        }
        else
        {
            AttackFloat = 1;
        }
    }
    public void Attack()
    {
        DamageGiven = 0;
        EnemyHealthManager AttackedEnemy;
        // plays animator
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackSpace.position, AttackRange,EnemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            AttackedEnemy = enemy.GetComponentInParent<EnemyHealthManager>();
            StartCoroutine(AttackedEnemy.GotDamage(DamageGiven,1));
        }
    }

    // finds if player performs an attack
    void AttackTimer()
    {
        if(Attack1DurationValue <= Attack1Duration && IsAttacking)
        {
            Attack1DurationValue += Time.deltaTime;
        }
        else
        {
            Attack1DurationValue = 0;
            IsAttacking = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackSpace.position, AttackRange);
    }
}
