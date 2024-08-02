using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObjectABC : MonoBehaviour
{
    public float moveDistance = 3f; // Odległość, o którą drzwi mają się przesunąć
    public float moveTime = 2f;     // Czas, w którym drzwi mają się przesunąć

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;
    private bool isPaused = false;

    void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + Vector3.up * moveDistance;
    }

    public void ToggleDoor()
    {
        if (isPaused) return;

        isOpen = !isOpen;
        StopAllCoroutines();
        StartCoroutine(MoveDoor(isOpen ? openPosition : closedPosition));
    }

    private IEnumerator MoveDoor(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = transform.position;

        while (elapsedTime < moveTime)
        {
            if (!isPaused)
            {
                transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / moveTime);
                elapsedTime += Time.deltaTime;
            }
            yield return null;
        }

        transform.position = targetPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PullObject"))
        {
            isPaused = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PullObject"))
        {
            isPaused = false;
        }
    }
    
    
    
}
