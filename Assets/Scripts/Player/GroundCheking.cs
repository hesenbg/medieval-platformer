using UnityEngine;
public class GroundCheking : MonoBehaviour
{
    public bool IsPressing ;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("ground")){
            IsPressing = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("ground")){
            IsPressing = false;
        }
    }
}

