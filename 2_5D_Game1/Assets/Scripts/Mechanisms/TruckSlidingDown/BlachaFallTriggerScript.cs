using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlachaFallTriggerScript : MonoBehaviour
{
    public GameObject blachaFallObject;  // Obiekt, który posiada animator z parametrem "IsBlachaFalling"
    public GameObject truck1Object;  // Obiekt, który posiada animator z parametrem "IsChockGone"
    
    private Animator blachaAnimator;
    private Animator truck1Animator;

    void Start()
    {
        // Pobieranie komponentu Animator z obiektu BlachaFall
        blachaAnimator = blachaFallObject.GetComponent<Animator>();
        
        // Pobieranie komponentu Animator z obiektu Truck1
        truck1Animator = truck1Object.GetComponent<Animator>();
    }

    // Wykrywanie kolizji z triggerem
    private void OnTriggerEnter(Collider other)
    {
        // Sprawdzenie, czy obiekt, który wszedł w kolizję, to gracz (możesz zmienić warunek w zależności od potrzeb)
        if (other.CompareTag("Player"))
        {
            // Sprawdzanie czy IsChockGone w animatorze Truck1 jest true
            if (truck1Animator.GetBool("IsChockGone"))
            {
                // Ustawienie IsBlachaFalling na true
                blachaAnimator.SetBool("IsBlachaFalling", true);
            }
        }
    }
    
    
}
