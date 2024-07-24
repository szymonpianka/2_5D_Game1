using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing_v3 : MonoBehaviour
{
    public float moveDuration = 1.0f; // Czas w sekundach na przemieszczenie się
    public float horizontalOffset = 1.0f; // Wartość przesunięcia w bok
    public float verticalOffset = 1.0f; // Wartość przesunięcia w górę
    public List<GameObject> triggerObjects; // Lista obiektów, które aktywują przesunięcie
    PlayerMovement playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = gameObject.GetComponent<PlayerMovement>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (triggerObjects.Contains(other.gameObject))
        {
            StartCoroutine(MoveByOffset());
        }
    }

    IEnumerator MoveByOffset()
    {
        playerController.disabled = true;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + new Vector3(horizontalOffset, verticalOffset, 0);
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; // Upewnienie się, że obiekt kończy ruch dokładnie w pozycji docelowej
        Debug.Log("Movement completed");
        playerController.disabled = false;
    }
    
    
}
