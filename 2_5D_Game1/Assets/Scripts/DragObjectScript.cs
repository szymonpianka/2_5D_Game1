using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjectScript : MonoBehaviour
{
    private Transform playerTransform;
    public Transform newParentTransform; // Przypisz ten obiekt w inspektorze
    public bool isDragging = false;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                transform.SetParent(playerTransform);
                isDragging = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.E) && isDragging)
        {
            Debug.Log("E zwolniony");
            transform.SetParent(newParentTransform); // Przypisanie nowego rodzica
            isDragging = false;
        }
    }
}
