using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgePart : MonoBehaviour
{
    public PowerSwitch powerSwitch; // Referencja do obiektu PowerSwitch
    public float moveDistance = 10f; // Dystans, na jaki mostek się przesunie
    public float moveTime = 2f; // Czas ruchu mostka
    private bool hasMoved = false; // Flaga sprawdzająca, czy mostek się już przesunął

    void Update()
    {
        if (powerSwitch.PowerOn && !hasMoved)
        {
            StartCoroutine(MoveBridgePart());
            hasMoved = true;
        }
    }

    private IEnumerator MoveBridgePart()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition - transform.right * moveDistance; // Ruch w lewo
        float elapsedTime = 0;

        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / moveTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;
    }
    
}
