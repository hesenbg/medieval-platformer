using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float CurrentHealth;
    public float MaxHealth;

    EnemyAI npc;

    private void Start()
    {
        npc = GetComponent<EnemyAI>();
    }

    public IEnumerator GotDamage(float Damage , float StunDuration)
    {
        npc.currentState = EnemyAI.State.Stunned;
        npc.AnimationManager.SetStunBool(true);

        CurrentHealth -= Damage;

        yield return new WaitForSeconds(StunDuration);

        npc.AnimationManager.SetStunBool(false);
        npc.currentState = EnemyAI.State.Fighting;
    }
}   