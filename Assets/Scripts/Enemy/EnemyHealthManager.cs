using System.Collections;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public float CurrentHealth;
    public float MaxHealth;

    EnemyAI npc;

    private void Start()
    {
        npc = GetComponent<EnemyAI>();

        CurrentHealth= MaxHealth;
    }

    private void Update()
    {
        CheckDead();
    }

    void CheckDead()
    {
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator GotDamage(float Damage , float StunDuration)
    {
        npc.currentState = EnemyAI.State.Stunned;
        npc.AnimationManager.StunAnimation(true);
        CurrentHealth -= Damage;

        yield return new WaitForSeconds(StunDuration);

        npc.AnimationManager.StunAnimation(false);
        npc.currentState = EnemyAI.State.Fighting;
    }
}   