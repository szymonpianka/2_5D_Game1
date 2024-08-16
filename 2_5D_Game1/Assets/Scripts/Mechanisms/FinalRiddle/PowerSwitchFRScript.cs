using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSwitchFRScript : MonoBehaviour
{
    public bool PowerFROn = false;
    private bool isPlayerInTrigger = false;

    void OnTriggerEnter(Collider other)
    {
        // Sprawdza, czy gracz wszedł w obszar kolidera
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Sprawdza, czy gracz opuścił obszar kolidera
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }

    void Update()
    {
        // Sprawdza, czy gracz jest w obszarze kolidera i czy wcisnął przycisk "Interact"
        if (isPlayerInTrigger && Input.GetButtonDown("Interact"))
        {
            Debug.Log("PowerFROn");
            PowerFROn = true;
        }
    }
    
    
}
