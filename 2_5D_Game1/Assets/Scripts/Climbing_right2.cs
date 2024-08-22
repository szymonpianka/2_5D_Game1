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
    public Vector3 climbBeginOffset; // Offset punktu początkowego wspinaczki względem obiektu, na którym jest skrypt

    private Animator animator;
    private PlayerMovement playerController;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = gameObject.GetComponent<PlayerMovement>();
    }

    void OnTriggerStay(Collider other)
    {
        // Sprawdzenie, czy gracz dotyka triggera i nie jest w trakcie spadania
        if (other.CompareTag("ClimbingTrigger") && !animator.GetBool("IsFalling") && playerController.isJumping)
        {
            animator.SetBool("IsClimbing", true);
            StartCoroutine(TeleportAndMove());
        }
    }

    IEnumerator TeleportAndMove()
    {
        // Wyłączenie sterowania graczem
        playerController.disabled = true;

        // Teleportacja gracza na podstawie offsetu względem pozycji obiektu, na którym jest skrypt
        Vector3 targetPosition = transform.position + transform.TransformVector(climbBeginOffset);
        playerController.transform.position = targetPosition;

        // Krótkie opóźnienie, aby gracz zobaczył efekt teleportacji (opcjonalne)
        yield return new WaitForSeconds(0.1f);

        // Ruch gracza do pierwszej pozycji
        Vector3 startPosition = playerController.transform.position;
        Vector3 firstTargetPosition = startPosition + new Vector3(horizontalOffset1, verticalOffset1, 0);
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration1)
        {
            playerController.transform.position = Vector3.Lerp(startPosition, firstTargetPosition, elapsedTime / moveDuration1);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Upewnienie się, że obiekt kończy ruch dokładnie w pierwszej pozycji
        playerController.transform.position = firstTargetPosition;

        // Resetowanie czasu
        elapsedTime = 0f;

        // Ruch gracza do drugiej pozycji
        Vector3 secondTargetPosition = firstTargetPosition + new Vector3(horizontalOffset2, verticalOffset2, 0);

        while (elapsedTime < moveDuration2)
        {
            playerController.transform.position = Vector3.Lerp(firstTargetPosition, secondTargetPosition, elapsedTime / moveDuration2);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Upewnienie się, że obiekt kończy ruch dokładnie w drugiej pozycji
        playerController.transform.position = secondTargetPosition;
        Debug.Log("Movement completed");

        // Włączenie sterowania graczem
        playerController.disabled = false;
        animator.SetBool("IsClimbing", false);
    }
    
    
    
    
}
