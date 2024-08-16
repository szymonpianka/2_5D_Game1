using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceCrushTriggerScript : MonoBehaviour
{
    // Obiekt Crusher, z którym sprawdzamy kolizję
    public GameObject crusher;

    // Lista obiektów, które mają zostać zniszczone
    public List<GameObject> objectsToDestroy;

    // Obiekt, który ma się pojawić
    public GameObject objectToActivate;

    private void OnTriggerEnter(Collider other)
    {
        // Sprawdzamy, czy kolizja wystąpiła z obiektem Crusher
        if (other.gameObject == crusher)
        {
            // Niszczenie wszystkich obiektów na liście
            foreach (GameObject obj in objectsToDestroy)
            {
                if (obj != null)
                {
                    Destroy(obj);
                }
            }

            // Aktywowanie innego obiektu
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }
        }
    }

   
}
