using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public FuseBox1 fusebox1; // Referencja do skryptu Fusebox1
    public float moveDistance = 10f;
    public float moveTime = 2f;
    private bool isPlayerInTrigger = false;
    private Transform playerTransform;
    private PlayerMovement playerController;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            playerTransform = other.transform;
            playerController = other.GetComponent<PlayerMovement>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            playerTransform = null;
            playerController = null;
        }
    }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E) && fusebox1 != null && fusebox1.ElevatorOn)
        {
            StartCoroutine(MoveElevator());
        }
    }

    private IEnumerator MoveElevator()
    {
        if (playerTransform != null)
        {
            playerTransform.SetParent(transform);
            if (playerController != null)
            {
                playerController.enabled = false; // Wyłączenie sterowania graczem
            }
        }

        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + transform.up * moveDistance; // Ruch w górę
        float elapsedTime = 0;

        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;

        if (playerTransform != null)
        {
            playerTransform.SetParent(null);
            if (playerController != null)
            {
                playerController.enabled = true; // Włączenie sterowania graczem
            }
        }
    }
    
    
   
}
