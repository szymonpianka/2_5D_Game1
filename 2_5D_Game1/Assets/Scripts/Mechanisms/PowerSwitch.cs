using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSwitch : MonoBehaviour
{
    public bool PowerOn = false;
    private bool isPlayerInTrigger = false;

    // Listy do przechowywania referencji do świateł
    public List<Light> lightsToEnable;
    public List<Light> lightsToDisable;

    void Start()
    {
        // Ustawienie wszystkich świateł z listy lightsToEnable jako wyłączone na początku gry
        foreach (Light light in lightsToEnable)
        {
            light.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetButtonDown("Interact"))
        {
            PowerOn = true;
            Debug.Log("Power is ON");

            // Włączanie świateł z pierwszej listy
            foreach (Light light in lightsToEnable)
            {
                light.enabled = true;
            }

            // Wyłączanie świateł z drugiej listy
            foreach (Light light in lightsToDisable)
            {
                light.enabled = false;
            }
        }
    }
    
    
   
}
