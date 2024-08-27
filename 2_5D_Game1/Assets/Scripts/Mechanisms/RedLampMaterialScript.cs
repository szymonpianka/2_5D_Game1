using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLampMaterialScript : MonoBehaviour
{
    public GameObject secondObject;  // Odwołanie do drugiego obiektu, na którym jest skrypt PowerSwitch
    public Material newMaterial;     // Materiał, na który zmieni się obiekt
    private Renderer objectRenderer; // Renderer obiektu, na którym zmienimy materiał
    private PowerSwitch powerSwitch; // Odwołanie do skryptu PowerSwitch

    void Start()
    {
        // Pobieramy renderer z aktualnego obiektu
        objectRenderer = GetComponent<Renderer>();

        // Pobieramy skrypt PowerSwitch z drugiego obiektu
        powerSwitch = secondObject.GetComponent<PowerSwitch>();
    }

    void Update()
    {
        // Sprawdzamy wartość PowerOn z drugiego obiektu
        if (powerSwitch != null && objectRenderer != null)
        {
            // Jeśli PowerOn jest true, zmieniamy materiał obiektu na nowy materiał
            if (powerSwitch.PowerOn)
            {
                objectRenderer.material = newMaterial;
            }
        }
    }
    
}
