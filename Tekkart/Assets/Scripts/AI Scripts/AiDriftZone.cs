﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDriftZone : MonoBehaviour
{
    public float DriftSize = 0;

    //0 = small, 2 = med, 4 large 

    private void OnTriggerEnter(Collider other)
    {
        AIScript Kart = other.GetComponent<AIScript>();

        if (Kart != null)
        {
            Kart.EnterDriftZone(DriftSize);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AIScript Kart = other.GetComponent<AIScript>();

        if (Kart != null)
        {
            Kart.ExitDriftZone();
        }
    }
}
