using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour
{
    // Referencja do komponentu Renderer obiektu
    private Renderer objectRenderer;

    // Inicjalizacja
    void Start()
    {
        // Pobranie komponentu Renderer przypisanego do obiektu
        objectRenderer = GetComponent<Renderer>();
    }

    // Aktualizacja na każdej klatce
    void Update()
    {
        // Sprawdzenie, czy przycisk "E" jest wciśnięty
        if (Input.GetKey(KeyCode.E))
        {
            // Ukrycie obiektu
            objectRenderer.enabled = false;
        }
        else
        {
            // Wyświetlenie obiektu
            objectRenderer.enabled = true;
        }
    }
}
