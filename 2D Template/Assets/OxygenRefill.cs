using UnityEngine;

public class OxygenRefill : MonoBehaviour
{
    [System.Obsolete]
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Timer timer = FindObjectOfType<Timer>();
            if (timer != null)
            {
                timer.RemainingTime += 30f; // Add 30 seconds to the timer
            }
            Destroy(gameObject); // Remove the oxygen refill object
        }
    }
}
