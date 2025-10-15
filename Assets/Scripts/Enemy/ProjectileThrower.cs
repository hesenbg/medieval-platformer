using UnityEngine;
public class ProjectileThrower : MonoBehaviour
{
    public GameObject Arrow;
    public Guard Guard;
    [SerializeField] AnimationClip AttackAnimation;
    [SerializeField] float MaxTime = 1.5f;
    Transform ArrowTransform;
    float CurrentTime = 0f;

    Vector3 ArrowScale;

    private void Awake()
    {
        ArrowTransform = Guard.transform;
        ArrowScale = new Vector3(0.2f, 0.3f, 0.2f);
        //ArrowTransform.localScale = ArrowScale;
        MaxTime = AttackAnimation.length;
    }

    private void Update()
    {
        ShootArrow();
    }

    void ShootArrow()
    {
        if (Arrow != null)
        {
            if (CurrentTime < MaxTime)
            {
                CurrentTime += Time.deltaTime;
            }
            else
            {
                CurrentTime = 0f;
                if (Guard.currentState == Guard.GuardState.Fighting)
                {
                    Instantiate(Arrow,Guard.transform.position,Guard.transform.rotation);
                }
            }
        }
    }

}
