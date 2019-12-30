using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Kart ThisKart = other.gameObject.GetComponent<Kart>();


        if (ThisKart != null)
        {
            ThisKart.Boost();
        } 
    }
}
