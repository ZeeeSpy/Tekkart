using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCrate : MonoBehaviour
{
    private MeshRenderer MR;
    private BoxCollider BC;

    private void Awake()
    {
        MR = GetComponent<MeshRenderer>();
        BC = GetComponent<BoxCollider>();

    }
    private void OnTriggerEnter(Collider other)
    {
        Pickupable ThisKart = other.gameObject.GetComponent<Pickupable>();

        if (ThisKart != null)
        {
            StartCoroutine("Reset");
            ThisKart.GetPickUp();
        }
    }

    IEnumerator Reset()
    {
        MR.enabled = false;
        BC.enabled = false;
        yield return new WaitForSeconds(5f);
        MR.enabled = true;
        BC.enabled = true;
    }
}
