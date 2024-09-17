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
    public bool requireKeyPress = true;          // Czy wymagane jest wciśnięcie klawisza E?
    public KeyCode activationKey = KeyCode.E;    // Klawisz aktywacji, jeśli requireKeyPress jest true

    private bool isPlayerInRange = false;        // Czy gracz jest w zasięgu?
    private Coroutine currentSequenceCoroutine;

    void Update()
    {
        if (requireKeyPress)
        {
            // Sprawdzenie, czy gracz jest w zasięgu i czy naciśnięto klawisz aktywacji
            if (isPlayerInRange && Input.GetKeyDown(activationKey))
            {
                StartSequence();
            }
        }
        else
        {
            // Jeśli nie wymagane jest wciśnięcie klawisza
            if (isPlayerInRange)
            {
                StartSequence();
            }
        }
    }

    private void StartSequence()
    {
        // Jeśli korutyna już działa, zatrzymaj ją
        if (currentSequenceCoroutine != null)
        {
            StopCoroutine(currentSequenceCoroutine);
        }

        // Rozpocznij nową sekwencję akcji
        currentSequenceCoroutine = StartCoroutine(ExecuteAnimatorSequence());
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
