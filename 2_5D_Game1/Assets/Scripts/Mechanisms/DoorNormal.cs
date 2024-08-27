using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNormal : MonoBehaviour
{
    public GameObject objectToMove; // Obiekt, który ma się poruszyć
    public float verticalOffset = 3f; // Odległość, na którą obiekt ma się przesunąć (góra/dół)
    public float moveSpeed = 2f; // Prędkość ruchu w jednostkach na sekundę
    public float delayBeforeMove = 1f; // Opóźnienie przed rozpoczęciem ruchu

    private Vector3 initialPosition; // Początkowa pozycja obiektu
    private Vector3 targetPosition; // Docelowa pozycja obiektu
    private Coroutine currentMoveCoroutine; // Aktualna działająca korutyna
    private bool movingUp = true; // Czy obiekt porusza się w górę?
    private bool isPlayerInRange = false; // Czy gracz jest w zasięgu?

    void Start()
    {
        // Ustawienie początkowej pozycji na aktualną pozycję obiektu
        initialPosition = objectToMove.transform.position;
        // Ustawienie docelowej pozycji na podstawie początkowej pozycji i verticalOffset
        targetPosition = initialPosition + Vector3.up * verticalOffset;
    }

    void Update()
    {
        // Sprawdzenie, czy gracz jest w zasięgu i czy naciśnięto klawisz E
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Jeśli korutyna ruchu już działa, zatrzymaj ją
            if (currentMoveCoroutine != null)
            {
                StopCoroutine(currentMoveCoroutine);
            }

            // Rozpocznij nową korutynę, która odczeka i dopiero potem rozpocznie ruch
            currentMoveCoroutine = StartCoroutine(DelayedMove());
        }
    }

    private IEnumerator DelayedMove()
    {
        // Odczekaj określoną ilość czasu
        yield return new WaitForSeconds(delayBeforeMove);

        // Sprawdź, w którą stronę obiekt powinien się poruszać
        if (movingUp)
        {
            currentMoveCoroutine = StartCoroutine(MoveObject(objectToMove.transform.position, targetPosition));
        }
        else
        {
            currentMoveCoroutine = StartCoroutine(MoveObject(objectToMove.transform.position, initialPosition));
        }

        // Zmiana stanu kierunku
        movingUp = !movingUp;
    }

    private IEnumerator MoveObject(Vector3 start, Vector3 end)
    {
        float distance = Vector3.Distance(start, end); // Obliczenie odległości do przebycia
        float duration = distance / moveSpeed; // Obliczenie czasu potrzebnego na pokonanie tej odległości
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            objectToMove.transform.position = Vector3.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objectToMove.transform.position = end;
        currentMoveCoroutine = null; // Ruch zakończony, ustawienie korutyny na null
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
