using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChockFallTrigger : MonoBehaviour
{
    // Przypisz te obiekty w inspektorze
    public Animator truckAnimator; // Animator z Truck1
    public Animator wheelChockAnimator; // Animator z WheelChock

    private void OnTriggerEnter(Collider other)
    {
        // Sprawdź, czy obiekt, który wszedł w trigger, to WheelChock
        if (other.gameObject.CompareTag("WheelChock"))
        {
            // Ustaw booleany na true
            truckAnimator.SetBool("IsChockGone", true);
            wheelChockAnimator.SetBool("IsChockFalling", true);
        }
    }
   
}
