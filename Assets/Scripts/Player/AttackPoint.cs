using UnityEngine;
public class AttackPoint : MonoBehaviour
{
    public Player player;

    Vector3 pos;
    void LateUpdate()
    {
        pos = transform.position;

        transform.position.Set(pos.x * player.Direction, pos.y, pos.z);
    }
}
