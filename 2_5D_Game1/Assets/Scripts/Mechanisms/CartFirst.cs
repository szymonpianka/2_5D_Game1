using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartFirst : MonoBehaviour
{
    public Transform waypoint; // Punkt, do którego obiekt się przemieści
    public float moveTime = 2f;
    private bool hasMoved = false;
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
        // Sprawdzenie, czy gracz jest w zasięgu i wciśnięto klawisz E
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E) && !hasMoved)
        {
            StartCoroutine(MoveWagonik());
            hasMoved = true;
        }
    }

    private IEnumerator MoveWagonik()
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
        Vector3 endPosition = waypoint.position; // Użycie pozycji waypointu jako punktu docelowego
        float elapsedTime = 0;

        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / moveTime));
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
