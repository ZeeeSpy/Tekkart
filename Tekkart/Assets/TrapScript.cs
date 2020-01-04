using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Kart ThisKart = other.gameObject.GetComponent<Kart>();

        if (ThisKart != null)
        {
            ThisKart.BecomeStunned();
            Destroy(gameObject);
        }
    }
}
