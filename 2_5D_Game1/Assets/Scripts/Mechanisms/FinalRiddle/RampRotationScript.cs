using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampRotationScript : MonoBehaviour
{
    public GameObject objectToRotate; // Obiekt, który ma się obracać
    public float rotationAngle = 90f; // Kąt obrotu w stopniach
    public float rotationSpeed = 30f; // Prędkość rotacji w stopniach na sekundę
    public PowerSwitchFRScript powerSwitch; // Odwołanie do skryptu PowerSwitchFRScript

    private bool isPlayerInRange = false; // Czy gracz jest w zasięgu
    private bool isRotatedToSecondPoint = false; // Czy obiekt jest obrócony do drugiego punktu rotacji
    private bool isRotating = false; // Czy obiekt aktualnie się obraca
    private Quaternion firstRotation; // Początkowa rotacja obiektu (pierwszy punkt)
    private Quaternion secondRotation; // Docelowa rotacja obiektu (drugi punkt)

    void Start()
    {
        // Zapisz początkową rotację obiektu jako pierwszy punkt rotacji
        firstRotation = objectToRotate.transform.rotation;

        // Ustal drugi punkt rotacji jako sumę początkowej rotacji i kąta z inspektora
        secondRotation = firstRotation * Quaternion.Euler(0, 0, rotationAngle);
    }

    void Update()
    {
        // Sprawdź, czy gracz jest w zasięgu, przycisk E został naciśnięty, obiekt nie jest już w ruchu, i czy PowerFROn jest true
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !isRotating && powerSwitch != null && powerSwitch.PowerFROn)
        {
            if (isRotatedToSecondPoint)
            {
                StartCoroutine(RotateObject(firstRotation)); // Obróć do pierwszego punktu
            }
            else
            {
                StartCoroutine(RotateObject(secondRotation)); // Obróć do drugiego punktu
            }
            isRotatedToSecondPoint = !isRotatedToSecondPoint; // Zmień stan rotacji
        }
    }

    private IEnumerator RotateObject(Quaternion targetRotation)
    {
        isRotating = true; // Ustaw flagę, że obiekt się obraca
        Quaternion startRotation = objectToRotate.transform.rotation;
        
        // Oblicz kąt między obecną rotacją a docelową rotacją
        float angleDifference = Quaternion.Angle(startRotation, targetRotation);
        
        // Oblicz czas na podstawie kąta i prędkości
        float rotationTime = angleDifference / rotationSpeed;
        
        float elapsedTime = 0f;

        while (elapsedTime < rotationTime)
        {
            float t = elapsedTime / rotationTime;
            objectToRotate.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ustaw rotację na końcowy stan
        objectToRotate.transform.rotation = targetRotation;
        isRotating = false; // Zresetuj flagę po zakończeniu obrotu
    }

    public void StopRotation()
    {
        if (isRotating)
        {
            StopAllCoroutines(); // Zatrzymaj aktualną korutynę
            isRotating = false; // Zresetuj flagę po zatrzymaniu rotacji
        }
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
