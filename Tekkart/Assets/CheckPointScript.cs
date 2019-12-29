using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    private LapNumber LapParent;
    private int position = -1;
    private KartScript ThisKart;
    private AIScript AIKart;

    public void SetUpPosition(int masterposition, LapNumber IncLapParent)
    {
        position = masterposition;
        LapParent = IncLapParent;
    }

    private void OnTriggerEnter(Collider other)
    {
        ThisKart = other.gameObject.GetComponent<KartScript>();


        if (ThisKart != null)
        {
            LapParent.CheckIn(position);
            return;
        }

        /*
        AIKart = other.gameObject.GetComponent<AIScript>();

        if (AIKart != null)
        {
            Debug.Log("Got To Here");
            AIKart.CheckPointReached();
        }
        */
    }
}
