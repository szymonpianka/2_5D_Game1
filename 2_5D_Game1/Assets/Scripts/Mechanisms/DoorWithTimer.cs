using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWithTimer : MonoBehaviour
{
    public GameObject objectToMove; // Obiekt, który ma się poruszyć
    public float moveDistance = 3f; // Odległość, na którą obiekt ma się poruszyć
    public float moveTime = 2f; // Czas, w którym obiekt ma się poruszyć
    public float waitTime = 2f; // Czas oczekiwania przed powrotem

    private bool isPlayerInRange = false; // Czy gracz jest w zasięgu
    private bool isMoving = false; // Czy obiekt się porusza
    private bool isPaused = false; // Czy ruch jest zatrzymany

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !isMoving)
        {
            StartCoroutine(MoveObjectUpAndDown());
        }
    }

    private IEnumerator MoveObjectUpAndDown()
    {
        isMoving = true;

        Vector3 startPosition = objectToMove.transform.position;
        Vector3 endPosition = startPosition + Vector3.up * moveDistance;
        float elapsedTime = 0f;

        // Ruch w górę
        while (elapsedTime < moveTime)
        {
            if (!isPaused)
            {
                objectToMove.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveTime);
                elapsedTime += Time.deltaTime;
            }
            yield return null;
        }
        objectToMove.transform.position = endPosition;

        // Czekanie
        float waitElapsedTime = 0f;
        while (waitElapsedTime < waitTime)
        {
            if (!isPaused)
            {
                waitElapsedTime += Time.deltaTime;
            }
            yield return null;
        }

        elapsedTime = 0f;

        // Ruch w dół
        while (elapsedTime < moveTime)
        {
            if (!isPaused)
            {
                objectToMove.transform.position = Vector3.Lerp(endPosition, startPosition, elapsedTime / moveTime);
                elapsedTime += Time.deltaTime;
            }
            yield return null;
        }
        objectToMove.transform.position = startPosition;

        isMoving = false;
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

    public void SetPaused(bool paused)
    {
        isPaused = paused;
    }
}
