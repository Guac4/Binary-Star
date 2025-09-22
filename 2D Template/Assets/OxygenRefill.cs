using UnityEngine;

public class OxygenRefill : MonoBehaviour
{
    [System.Obsolete]
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Destroy(gameObject); // Remove the oxygen refill object
        }
    }
}
