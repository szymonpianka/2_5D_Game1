using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampScript : MonoBehaviour
{
    private RampRotationScript leverScript;

    void Start()
    {
        leverScript = FindObjectOfType<RampRotationScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PullObject"))
        {
            leverScript.StopRotation(); // Zatrzymaj rotację całkowicie
        }
    }

    
    
}
