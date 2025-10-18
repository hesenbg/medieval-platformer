using System.Threading.Tasks;
using UnityEngine;
public class EnemyCombat : MonoBehaviour
{
    public GameObject Arrow;
    EnemyAI enemy;
    [SerializeField] float AttackDelay = 1.5f;
    float CurrentTime = 0f;
    [SerializeField] float SlashDistance;
    [SerializeField] float SlashDamage;
    EnemyAnimationManager animationManager;


    private void Awake()
    {
        enemy = GetComponent<EnemyAI>();

        animationManager = gameObject.GetComponentInParent<EnemyAnimationManager>();
    }

    public void Attack()
    {
        if (CurrentTime < AttackDelay)
        {
            CurrentTime += Time.deltaTime;
        }
        else
        {
            CurrentTime = 0f;

            if(enemy.CurrentEnemyType == EnemyAI.EnemyType.melee)
            {
                // aplly logic
                SlashSword();
                // animation
                animationManager.PlayAttack();
            }
            else
            {
                ShootArrow();
            }
        }
    }

    private void ShootArrow()
    {
        Instantiate(Arrow, enemy.transform.position, enemy.transform.rotation);
    }

    public void SlashSword()
    {
        // apply logic
        RaycastHit2D Hit;
        Player player;

        Hit= Physics2D.CircleCast(transform.position,
            SlashDistance,
            transform.right);

        if(Hit.collider.gameObject.layer == 6) { 
            player = Hit.collider.gameObject.GetComponent<Player>();
            player.CurrentHealth -= SlashDamage;
        }
    }
}

