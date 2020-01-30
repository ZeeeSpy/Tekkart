using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnguidedMissleScript : MonoBehaviour
{
    private  Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine("DestroySelf");
        rb.AddForce(transform.forward * -50, ForceMode.Impulse );
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(transform.forward * -20);
    }

    private void OnTriggerEnter(Collider other)
    {
        Kart ThisKart = other.gameObject.GetComponent<Kart>();

        if (ThisKart != null)
        {
            ThisKart.BecomeStunned();
            Destroy(gameObject);
        }
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
