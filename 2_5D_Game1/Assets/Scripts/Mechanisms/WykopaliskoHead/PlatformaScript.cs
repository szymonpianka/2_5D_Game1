using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformaScript : MonoBehaviour
{
    public GameObject objectToMove; // Obiekt, który ma się poruszyć
    public float distance = 5f; // Odległość, na którą obiekt ma się poruszyć
    public float speed = 2f; // Prędkość poruszania się obiektu
    public float delay = 2f; // Czas opóźnienia przed wykonaniem ruchu

    private Vector3 targetPosition; // Docelowa pozycja
    private bool isMoving = false; // Flaga, czy obiekt ma się poruszać
    private bool hasMoved = false; // Flaga, czy obiekt już się poruszył (aktywacja tylko raz)
    private bool isPlayerInTrigger = false; // Flaga, czy gracz jest w triggerze

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true; // Gracz w triggerze
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false; // Gracz opuszcza trigger
        }
    }

    void Update()
    {
        // Sprawdzanie, czy gracz jest w triggerze, naciśnięto "E" oraz czy akcja jeszcze nie została wykonana
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E) && !hasMoved)
        {
            if (!isMoving)
            {
                hasMoved = true; // Ustawienie flagi, że ruch został wykonany
                StartCoroutine(MoveObjectWithDelay()); // Rozpoczęcie ruchu z opóźnieniem
            }
        }
    }

    private IEnumerator MoveObjectWithDelay()
    {
        // Odczekanie określonego czasu
        yield return new WaitForSeconds(delay);

        // Ustawienie docelowej pozycji (poruszanie w dół)
        targetPosition = objectToMove.transform.position - new Vector3(0, distance, 0);
        isMoving = true;

        // Ruch obiektu
        while (isMoving)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, targetPosition, speed * Time.deltaTime);

            // Sprawdzenie, czy obiekt osiągnął docelową pozycję
            if (objectToMove.transform.position == targetPosition)
            {
                isMoving = false;
            }

            yield return null; // Kontynuowanie w kolejnym klatce
        }
    }
   
    
    
}
