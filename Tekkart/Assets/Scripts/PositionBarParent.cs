using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PositionBarParent : MonoBehaviour
{
    private PositionBarScript[] BarArray;
    public Canvas ParentCanvas;
    private PlayerPointScript PPS;
    private bool Waiting = false;

    private void Awake()
    {
        BarArray = new PositionBarScript[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            BarArray[i] = transform.GetChild(i).GetComponent<PositionBarScript>();
        }
    }

    public void ChangeBarValues(string[,] IncPositions, PlayerPointScript IncPPS)
    {
        PlayerPrefs.SetInt("RACES_COMPLETE", (PlayerPrefs.GetInt("RACES_COMPLETE")+1));
        Array.Reverse(BarArray);
        PPS = IncPPS;
        int j = 0;
        int q = 9;
        foreach (PositionBarScript i in BarArray)
        {
            i.SetUpBar(q.ToString(), IncPositions[j, 0], IncPositions[j, 1]);
            j++;
            q--;
            //The array is reversed but the 2d incoming array isn't
            //Simplest solution to the problem is two ints 
        }
        ParentCanvas.enabled = true;
        Waiting = true;
    }


    private void Update()
    {
        if (Waiting)
        {
            if (Input.GetButtonDown("Submit"))
            {
                ParentCanvas.enabled = false;
                PPS.LoadNext();
            }
        }
    }
}
