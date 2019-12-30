using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    private LapNumber LapParent;
    private int position = -1;
    private Kart ThisKart;

    public void SetUpPosition(int masterposition, LapNumber IncLapParent)
    {
        position = masterposition;
        LapParent = IncLapParent;
    }

    private void OnTriggerEnter(Collider other)
    {
        ThisKart = other.gameObject.GetComponent<Kart>();


        if (ThisKart != null)
        {
            LapParent.CheckIn(position, ThisKart);
        }
    }
}
