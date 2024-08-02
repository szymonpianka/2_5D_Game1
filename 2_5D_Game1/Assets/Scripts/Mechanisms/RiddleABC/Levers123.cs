using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levers123 : MonoBehaviour
{
    public DoorObjectABC doorA;
    public DoorObjectABC doorB;
    public DoorObjectABC doorC;
    public int leverNumber;

    private bool isPlayerInRange = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleDoors();
        }
    }

    private void ToggleDoors()
    {
        switch (leverNumber)
        {
            case 1:
                doorA.ToggleDoor();
                doorC.ToggleDoor();
                break;
            case 2:
                doorB.ToggleDoor();
                doorC.ToggleDoor();
                break;
            case 3:
                doorA.ToggleDoor();
                doorB.ToggleDoor();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
    
    
}
