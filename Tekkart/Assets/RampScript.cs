using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampScript : MonoBehaviour
{
    private KartScript ThisKart;
    private void OnTriggerEnter(Collider other)
    {
        ThisKart = other.gameObject.GetComponent<KartScript>();


        if (ThisKart != null)
        {
            ThisKart.Boost();
        } 
    }
}
