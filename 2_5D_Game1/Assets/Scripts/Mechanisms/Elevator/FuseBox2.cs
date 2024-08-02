using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox2 : MonoBehaviour
{
    public bool FuseTaken { get; private set; } = false;
    private bool playerInRange = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            FuseTaken = true;
            Debug.Log("FuseTaken");
        }
    }
    
    
    
}
