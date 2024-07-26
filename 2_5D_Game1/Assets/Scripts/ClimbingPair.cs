using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClimbingPair
{
    public List<GameObject> triggerObjects; // Lista obiektów, które aktywują przesunięcie
    public GameObject targetObject; // Obiekt, do którego gracz się teleportuje
}

