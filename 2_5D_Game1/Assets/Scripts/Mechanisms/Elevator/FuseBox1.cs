using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox1 : MonoBehaviour
{
    public FuseBox2 fusebox2; // Referencja do skryptu Fusebox2
    public bool ElevatorOn { get; private set; } = false;

    private bool playerInRange = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player in range of FuseBox1");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player left range of FuseBox1");
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && fusebox2 != null && fusebox2.FuseTaken)
        {
            ElevatorOn = true;
            Debug.Log("ElevatorOn = true");
        }
    }
    
    
    
}
