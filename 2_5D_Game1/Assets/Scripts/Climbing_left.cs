using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing_left : MonoBehaviour
{
    public float moveDuration = 1.0f; // Czas w sekundach na przemieszczenie się
    public float horizontalOffset = 1.0f; // Wartość przesunięcia w bok
    public float verticalOffset = 1.0f; // Wartość przesunięcia w górę
    public Vector3 climbBeginOffset; // Offset punktu początkowego wspinaczki względem obiektu, na którym jest skrypt

    private Animator animator;
    private PlayerMovement playerController;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = gameObject.GetComponent<PlayerMovement>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Sprawdzenie, czy gracz dotyka triggera z tagiem "ClimbingTriggerLeft" i nie jest w trakcie spadania
        if (other.CompareTag("ClimbingTriggerLeft") && !animator.GetBool("IsFalling"))
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

        // Ruch gracza po teleportacji
        Vector3 startPosition = playerController.transform.position;
        Vector3 finalPosition = startPosition + new Vector3(horizontalOffset, verticalOffset, 0);
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            playerController.transform.position = Vector3.Lerp(startPosition, finalPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Upewnienie się, że obiekt kończy ruch dokładnie w pozycji docelowej
        playerController.transform.position = finalPosition;
        Debug.Log("Movement completed");

        // Włączenie sterowania graczem
        playerController.disabled = false;
        animator.SetBool("IsClimbing", false);
    }
    
}
