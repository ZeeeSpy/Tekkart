using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTrialScript : MonoBehaviour, LapManager
{
    public Text LapNumberUI;
    public int LapLength = 3;

    private int NumberOfCheckpoints = -1;
    private Kart[] Players;
    private Kart PCKart;
    private List<Kart> KartsToCheck = new List<Kart>();
    private Dictionary<string, float> PlayerPositions = new Dictionary<string, float>();
    private Vector3[] CheckPointLocations;

    private bool newlap = false;
    private float currenttime = 0;
    private bool firstcheckin = true;

    private void Awake()
    {
        CheckPointScript[] CheckpointArray = GetComponentsInChildren<CheckPointScript>();
        NumberOfCheckpoints = CheckpointArray.Length;
        CheckPointLocations = new Vector3[CheckpointArray.Length];
        int i = 0;
        foreach (CheckPointScript Checkpoint in CheckpointArray)
        {
            Checkpoint.SetUpPosition(i, this);
            CheckPointLocations[i] = Checkpoint.gameObject.transform.position;
            i++;
        }
    }

    public void CheckIn(int inccheckpoint, Kart ThisKart)
    {
        if (firstcheckin)
        {
            firstcheckin = false;
            //Do stuff pre first lap
        }

        if (inccheckpoint == ThisKart.GetTargetCheckPoint())
        {
            ThisKart.SetTargetCheckPoint(inccheckpoint + 1);
            ThisKart.SetCheckPointValue(0.001f);
            ThisKart.SetCurrentCheckpoint(inccheckpoint);

            if (ThisKart.GetIsNewLap())
            {
                ThisKart.SetIsNewLap(false);
                ThisKart.SetCheckPointValue(1);
                if (ThisKart.GetName() == "Player")
                {
                    LapNumberUI.text = "Lap " + ((int)ThisKart.GetCheckPointValue()).ToString() + "/" + LapLength;
                }
            }

            if (ThisKart.GetTargetCheckPoint() == NumberOfCheckpoints)
            {
                ThisKart.SetTargetCheckPoint(0);
                ThisKart.SetIsNewLap(true);
            }
        }
        PlayerPositions[ThisKart.GetName()] = ThisKart.GetCheckPointValue();
    }

    void Update()
    {
        //count time to finish lap
    }
}
