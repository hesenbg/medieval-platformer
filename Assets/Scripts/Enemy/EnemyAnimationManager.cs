using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Guard Guard;
    private void Update()
    {
        AnimationSwitcher();
    }
    void AnimationSwitcher()
    {
        switch (Guard.currentState)
        {
            case Guard.GuardState.Wander:
                RunAnimation();
                break;
            case Guard.GuardState.Fighting:
                FightAnimation();
                break;
        }
    }
    public void SetStunBool(bool stun)
    {
        if (animator == null) return;

        animator.SetBool("IsStunned", stun);
    }

    void RunAnimation()
    {
        if (animator == null) return;

        animator.SetBool("IsAttacking", false);
    }

    void FightAnimation()
    {
        if (animator == null) return;

        animator.SetBool("IsAttacking", true);
    }
}
