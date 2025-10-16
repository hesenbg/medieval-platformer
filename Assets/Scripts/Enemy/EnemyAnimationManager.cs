using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] EnemyAI Guard;
    private void Update()
    {
        AnimationSwitcher();
    }
    void AnimationSwitcher()
    {
        switch (Guard.currentState)
        {
            case EnemyAI.State.Wander:
                RunAnimation();
                break;
            case EnemyAI.State.Fighting:
                FightAnimation();
                break;
        }
    }
    public void StunAnimation(bool stun)
    {
        if (animator == null) return;
        
        animator.SetBool("IsStunned", stun);
    }

    public void RunAnimation()
    {
        if (animator == null) return;

        animator.SetBool("IsAttacking", false);
    }

    public void FightAnimation()
    {
        if (animator == null) return;

        animator.SetBool("IsAttacking", true);
    }
    public void PlayAttack()
    {
        animator.SetTrigger("Attack");
    }
}
