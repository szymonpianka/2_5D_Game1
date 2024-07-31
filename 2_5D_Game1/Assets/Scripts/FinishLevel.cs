using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FinishLevel : MonoBehaviour
{
  // Ta funkcja zostanie wywołana, gdy obiekt z tagiem "Player" dotknie obiektu Finish
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Pobierz indeks aktualnej sceny
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Załaduj następny poziom z buildu
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

}
