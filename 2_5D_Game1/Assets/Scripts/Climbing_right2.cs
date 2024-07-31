using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing_right2 : MonoBehaviour
{
    public float moveDuration = 1.0f; // Czas w sekundach na przemieszczenie się
    public float horizontalOffset = 1.0f; // Wartość przesunięcia w bok
    public float verticalOffset = 1.0f; // Wartość przesunięcia w górę
    public List<ClimbingPair> climbingPairs; // Lista par trigger/target
    private Animator animator;
    PlayerMovement playerController;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = gameObject.GetComponent<PlayerMovement>();
    }

    void OnTriggerEnter(Collider other)
    {
        foreach (ClimbingPair pair in climbingPairs)
        {
            if (pair.triggerObjects.Contains(other.gameObject) && !animator.GetBool("IsFalling"))
            {
                animator.SetBool("IsClimbing", true);
                StartCoroutine(TeleportAndMove(pair.targetObject));
                break;
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

        // Ruch gracza po teleportacji
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + new Vector3(horizontalOffset, verticalOffset, 0);
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Upewnienie się, że obiekt kończy ruch dokładnie w pozycji docelowej
        transform.position = targetPosition;
        Debug.Log("Movement completed");

        // Włączenie sterowania graczem
        playerController.disabled = false;
        animator.SetBool("IsClimbing", false);
    }
}
