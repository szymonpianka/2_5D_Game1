using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNormal : MonoBehaviour
{
    public GameObject objectToMove; // Obiekt, który ma się poruszyć
    public float moveDistance = 3f; // Odległość, na którą obiekt ma się poruszyć
    public float moveTime = 2f; // Czas, w którym obiekt ma się poruszyć
    private bool isPlayerInRange = false; // Czy gracz jest w zasięgu

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(MoveObjectUp());
        }
    }

    private IEnumerator MoveObjectUp()
    {
        Vector3 startPosition = objectToMove.transform.position;
        Vector3 endPosition = startPosition + Vector3.up * moveDistance;
        float elapsedTime = 0f;

        while (elapsedTime < moveTime)
        {
            objectToMove.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objectToMove.transform.position = endPosition;
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
