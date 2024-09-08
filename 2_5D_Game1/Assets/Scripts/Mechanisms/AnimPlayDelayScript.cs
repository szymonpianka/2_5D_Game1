using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlayDelayScript : MonoBehaviour
{
    [System.Serializable]
    public class AnimatorAction
    {
        public Animator animator;          // Animator, na którym zmieniamy parametr
        public string boolParameterName;   // Nazwa parametru booleana w Animatorze
        public float delay;                // Opóźnienie przed ustawieniem parametru
    }

    public List<AnimatorAction> animatorActions; // Lista akcji animatora
    private bool isPlayerInRange = false;        // Czy gracz jest w zasięgu?
    private Coroutine currentSequenceCoroutine;

    void Update()
    {
        // Sprawdzenie, czy gracz jest w zasięgu i czy naciśnięto klawisz E
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Jeśli korutyna już działa, zatrzymaj ją
            if (currentSequenceCoroutine != null)
            {
                StopCoroutine(currentSequenceCoroutine);
            }

            // Rozpocznij nową sekwencję akcji
            currentSequenceCoroutine = StartCoroutine(ExecuteAnimatorSequence());
        }
    }

    private IEnumerator ExecuteAnimatorSequence()
    {
        foreach (AnimatorAction action in animatorActions)
        {
            // Odczekaj opóźnienie dla każdej akcji
            yield return new WaitForSeconds(action.delay);

            // Ustawienie booleana w animatorze na true
            action.animator.SetBool(action.boolParameterName, true);
        }

        currentSequenceCoroutine = null; // Korutyna zakończona
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
