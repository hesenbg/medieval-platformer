using UnityEngine;
public class EnemyCombat : MonoBehaviour
{
    public GameObject Arrow;
    public EnemyAI enemy;
    [SerializeField] AnimationClip AttackAnimation;
    [SerializeField] float AttackDelay = 1.5f;
    Transform ArrowTransform;
    float CurrentTime = 0f;
    [SerializeField] float SlashDistance;
    [SerializeField] float SlashDamage;


    Vector3 ArrowScale;

    private void Awake()
    {
        ArrowTransform = transform;
        ArrowScale = new Vector3(0.2f, 0.3f, 0.2f);
    }

    public void Attack()
    {
        if (Arrow != null)
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
                    // aplly animation
                }
                else
                {
                    ShootArrow();
                }
            }
        }
    }

    private void ShootArrow()
    {
        Instantiate(Arrow, enemy.transform.position, enemy.transform.rotation);
    }

    private void SlashSword()
    {
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
