using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSwitch : MonoBehaviour
{
    public bool PowerOn = false;
    private bool isPlayerInTrigger = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetButtonDown("Interact"))
        {
            PowerOn = true;
            Debug.Log("Power is ON");
        }
    }
    
   
}
