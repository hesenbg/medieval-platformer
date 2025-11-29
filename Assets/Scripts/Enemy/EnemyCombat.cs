using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public GameObject Arrow;

    EnemyAI enemy;
    EnemyAnimationManager animationManager;
    GuardSight guardSight;

    [SerializeField] float AttackDelay = 1.5f;
    [SerializeField] float SlashDamage = 10f;

    float currentTime = 0f;

    private void Awake()
    {
        guardSight = GetComponentInChildren<GuardSight>();
        enemy = GetComponent<EnemyAI>();
        animationManager = GetComponentInParent<EnemyAnimationManager>();
    }

    public void Attack()
    {
        currentTime -= Time.deltaTime;
        if (currentTime > 0f) return;

        currentTime = AttackDelay;

        if (enemy.CurrentEnemyType == EnemyAI.EnemyType.melee)
        {
            SlashSword();
            animationManager.PlayAttack();
        }
        else
        {
            ShootArrow();
        }
    }

    private void ShootArrow()
    {
        Instantiate(Arrow, enemy.transform.position, enemy.transform.rotation);
    }

    private void SlashSword()
    {
        RaycastHit2D hit = Physics2D.CircleCast(
            transform.position,
            0.4f,
            transform.right,          // correct direction
            guardSight.MeleeRange     // correct distance
        );

        if (hit.collider != null && hit.collider.gameObject.layer == 6)
        {
            Player player = hit.collider.GetComponent<Player>();
            if (player != null)
                player.GetDamage(SlashDamage);
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.right * guardSight.MeleeRange, 0.4f);
    }
}
