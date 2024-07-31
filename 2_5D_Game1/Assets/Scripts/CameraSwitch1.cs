using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitch1 : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float priorityIncrease = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Gracz wszedł w obszar, zwiększ priorytet kamery
            if (virtualCamera != null)
            {
                virtualCamera.Priority = Mathf.RoundToInt(virtualCamera.Priority + priorityIncrease);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Gracz opuścił obszar, przywróć pierwotny priorytet kamery
            if (virtualCamera != null)
            {
                virtualCamera.Priority = Mathf.RoundToInt(virtualCamera.Priority - priorityIncrease);
            }
        }
    }
    
}
