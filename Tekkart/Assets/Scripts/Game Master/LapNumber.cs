using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LapNumber : MonoBehaviour
{
    public Text LapNumberUI;
    public Text PositionDebug;

    private int NumberOfCheckpoints = -1;
    private bool newlap = false;

    private GameObject[] Players;
    private Dictionary<string, float> PlayerPositions =  new Dictionary<string, float>();

    private void Awake()
    {
        Players = GameObject.FindGameObjectsWithTag("Characters");

        foreach (GameObject p in Players)
        {
            PlayerPositions.Add(p.name, 0);
        }

        CheckPointScript[] CheckpointArray = GetComponentsInChildren<CheckPointScript>();
        NumberOfCheckpoints = CheckpointArray.Length;
        int i = 0;
        foreach (CheckPointScript Checkpoint in CheckpointArray)
        {
            Checkpoint.SetUpPosition(i, this);
            i++;
        }
    }

    public void CheckIn(int inccheckpoint, Kart ThisKart)
    {
        if (inccheckpoint == ThisKart.GetTargetCheckPoint())
        {
            ThisKart.SetTargetCheckPoint(inccheckpoint + 1);
            ThisKart.SetCheckPointValue(0.01f);
            ThisKart.SetCurrentCheckpoint(inccheckpoint);

            if (ThisKart.GetIsNewLap())
            {
                ThisKart.SetIsNewLap(false);
                ThisKart.SetCheckPointValue(1);
                if (ThisKart.GetName() == "Player")
                {
                    LapNumberUI.text = "Lap " + ((int)ThisKart.GetCheckPointValue()).ToString() + "/∞";
                }
            }

            if (ThisKart.GetTargetCheckPoint() == NumberOfCheckpoints)
            {
                ThisKart.SetTargetCheckPoint(0);
                ThisKart.SetIsNewLap(true);
            }
        }

        PlayerPositions[ThisKart.GetName()] = ThisKart.GetCheckPointValue();
        if (ThisKart.GetName() == "Player")
        {

        }
    }

    private void Update()
    {
        
    }
}
