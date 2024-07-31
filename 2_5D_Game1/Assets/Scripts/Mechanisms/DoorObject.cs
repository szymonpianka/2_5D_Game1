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
        if (other.CompareTag("PullObject"))
        {
            Debug.Log("PullObjectTouched");
            leverScript.SetPaused(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PullObject"))
        {
            leverScript.SetPaused(false);
        }
    }
    
}
