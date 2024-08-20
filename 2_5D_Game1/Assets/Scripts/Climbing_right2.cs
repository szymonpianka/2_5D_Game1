using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing_right2 : MonoBehaviour
{
    public float moveDuration1 = 1.0f; // Czas w sekundach na przemieszczenie się do pierwszej pozycji
    public float moveDuration2 = 1.0f; // Czas w sekundach na przemieszczenie się do drugiej pozycji
    public float horizontalOffset1 = 1.0f; // Wartość przesunięcia w bok do pierwszej pozycji
    public float verticalOffset1 = 1.0f; // Wartość przesunięcia w górę do pierwszej pozycji
    public float horizontalOffset2 = 1.0f; // Wartość przesunięcia w bok do drugiej pozycji
    public float verticalOffset2 = 1.0f; // Wartość przesunięcia w górę do drugiej pozycji
    public List<ClimbingPair> climbingPairs; // Lista par trigger/target
    private Animator animator;
    private PlayerMovement playerController;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = gameObject.GetComponent<PlayerMovement>();
    }

    void OnTriggerStay(Collider other)
    {
        // Sprawdzenie, czy gracz nie jest w trakcie spadania
        if (!animator.GetBool("IsFalling"))
        {
            foreach (ClimbingPair pair in climbingPairs)
            {
                // Sprawdzenie, czy gracz dotyka jednego z triggerów i jest w trakcie skoku
                if (pair.triggerObjects.Contains(other.gameObject) && playerController.isJumping)
                {
                    animator.SetBool("IsClimbing", true);
                    StartCoroutine(TeleportAndMove(pair.targetObject));
                    break;
                }
            }
        }
    }

    IEnumerator TeleportAndMove(GameObject target)
    {
        // Wyłączenie sterowania graczem
        playerController.disabled = true;

        // Teleportacja gracza do określonego obiektu
        if (target != null)
        {
            transform.position = target.transform.position;
        }
        else
        {
            Debug.LogWarning("Teleport target is not set.");
        }

        // Krótkie opóźnienie, aby gracz zobaczył efekt teleportacji (opcjonalne)
        yield return new WaitForSeconds(0.1f);

        // Ruch gracza do pierwszej pozycji
        Vector3 startPosition = transform.position;
        Vector3 firstTargetPosition = startPosition + new Vector3(horizontalOffset1, verticalOffset1, 0);
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration1)
        {
            transform.position = Vector3.Lerp(startPosition, firstTargetPosition, elapsedTime / moveDuration1);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Upewnienie się, że obiekt kończy ruch dokładnie w pierwszej pozycji
        transform.position = firstTargetPosition;

        // Resetowanie czasu
        elapsedTime = 0f;

        // Ruch gracza do drugiej pozycji
        Vector3 secondTargetPosition = firstTargetPosition + new Vector3(horizontalOffset2, verticalOffset2, 0);

        while (elapsedTime < moveDuration2)
        {
            transform.position = Vector3.Lerp(firstTargetPosition, secondTargetPosition, elapsedTime / moveDuration2);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Upewnienie się, że obiekt kończy ruch dokładnie w drugiej pozycji
        transform.position = secondTargetPosition;
        Debug.Log("Movement completed");

        // Włączenie sterowania graczem
        playerController.disabled = false;
        animator.SetBool("IsClimbing", false);
    }
    
    
}
