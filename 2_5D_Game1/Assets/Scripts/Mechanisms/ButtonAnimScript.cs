using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimScript : MonoBehaviour
{
    public Material newMaterial; // Materiał, który ma być zastosowany podczas ruchu
    public float moveDistance = 2.0f; // Odległość, o którą obiekt ma się przesunąć
    public float moveSpeed = 1.0f; // Prędkość przesuwania obiektu
    public float delayBeforeReturn = 2.0f; // Czas, przez jaki obiekt ma pozostać w nowej pozycji

    private Material originalMaterial; // Pierwotny materiał obiektu
    private Vector3 originalPosition; // Początkowa pozycja obiektu
    private bool isInteracting = false;

    private bool isPlayerInRange = false; // Czy gracz jest w zasięgu?

    void Start()
    {
        // Zapamiętaj początkowy materiał i pozycję obiektu
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            originalMaterial = renderer.material;
        }
        originalPosition = transform.position;
    }

    void Update()
    {
        // Sprawdzenie, czy gracz jest w zasięgu i czy naciśnięto klawisz E
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !isInteracting)
        {
            StartCoroutine(MoveAndReturn());
             
        }
        

    
       
    }

    private IEnumerator MoveAndReturn()
    {
        isInteracting = true;

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // Zmień materiał na nowy
            renderer.material = newMaterial;
        }

        // Ruch w dół
        Vector3 targetPosition = originalPosition - new Vector3(0, moveDistance, 0);
        float elapsedTime = 0f;

        while (elapsedTime < moveDistance / moveSpeed)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / (moveDistance / moveSpeed)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;

        // Czekaj przed powrotem
        yield return new WaitForSeconds(delayBeforeReturn);

        // Ruch powrotny
        elapsedTime = 0f;

        while (elapsedTime < moveDistance / moveSpeed)
        {
            transform.position = Vector3.Lerp(targetPosition, originalPosition, (elapsedTime / (moveDistance / moveSpeed)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;

        // Przywróć pierwotny materiał
        if (renderer != null)
        {
            renderer.material = originalMaterial;
        }

        isInteracting = false;
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
