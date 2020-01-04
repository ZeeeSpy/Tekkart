﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirStrikeScript : MonoBehaviour
{
    public Transform target;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HitSomething");
        Kart HitKart = other.gameObject.GetComponent<Kart>();

        if (HitKart != null)
        {
            HitKart.BecomeStunned();
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
        if (transform.position.y < 0)
        {
            Destroy(gameObject);
        }
    }
}
