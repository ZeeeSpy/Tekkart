using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapNumber : MonoBehaviour
{
    public Text LapNumberUI; 

    private int TargetCheckpoint = 0;
    private int NumberOfCheckpoints = -1;
    private bool newlap = false;
    private int lapnumber = 1;

    private void Awake()
    {
        CheckPointScript[] CheckpointArray = GetComponentsInChildren<CheckPointScript>();
        NumberOfCheckpoints = CheckpointArray.Length;
        int i = 0;
        foreach (CheckPointScript Checkpoint in CheckpointArray)
        {
            Checkpoint.SetUpPosition(i, this);
            i++;
        }
    }

    public void CheckIn(int inccheckpoint)
    {
        if (inccheckpoint == TargetCheckpoint)
        {
            TargetCheckpoint++;

            if (newlap)
            {
                newlap = false;
                lapnumber++;
                LapNumberUI.text = "Lap " + lapnumber.ToString() + "/∞";
            }

            if (TargetCheckpoint == NumberOfCheckpoints)
            {
                TargetCheckpoint = 0;
                newlap = true;
            }
        }
    }
}
