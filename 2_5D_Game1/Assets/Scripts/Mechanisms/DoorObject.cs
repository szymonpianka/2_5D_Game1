using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObject : MonoBehaviour
{
    private DoorWithTimer leverScript;

    void Start()
    {
        leverScript = FindObjectOfType<DoorWithTimer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DoorBlockade"))
        {
            Debug.Log("DoorBlockadeTouched");
            leverScript.SetPaused(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DoorBlockade"))
        {
            leverScript.SetPaused(false);
        }
    }
    
}
