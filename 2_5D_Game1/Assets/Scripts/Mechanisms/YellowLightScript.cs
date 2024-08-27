using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowLightScript : MonoBehaviour
{
    public GameObject secondObject;  // Odwołanie do drugiego obiektu
    private Animator animator;
    private PowerSwitch powerSwitch;  // Zmieniono na PowerSwitch

    void Start()
    {
        // Pobieramy Animator z aktualnego obiektu
        animator = GetComponent<Animator>();

        // Pobieramy skrypt PowerSwitch z drugiego obiektu
        powerSwitch = secondObject.GetComponent<PowerSwitch>();
    }

    void Update()
    {
        // Sprawdzamy wartość PowerOn z drugiego obiektu
        if (powerSwitch != null)
        {
            // Ustawiamy parametr w animatorze na podstawie stanu PowerOn
            animator.SetBool("AreYellowLightsOn", powerSwitch.PowerOn);
        }
    }
    
}
   

