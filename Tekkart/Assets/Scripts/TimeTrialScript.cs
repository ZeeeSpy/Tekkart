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
    private bool firstcheckin = false;
    private bool newlaptimer = false;
    private int currentlap = 0;

    private double[] TimeList = new double[3];

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
        if (!firstcheckin)
        {
            firstcheckin = true;
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
                newlaptimer = true;
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
       if (firstcheckin)
        {
            currenttime = currenttime + Time.deltaTime;
            if (newlaptimer)
            {
                newlaptimer = false;
                Debug.Log(currenttime);
                TimeList[currentlap] = (double)currenttime;
                currentlap = currentlap + 1;
                currenttime = 0;
                if (currentlap == 4)
                {
                    foreach (double n in TimeList)
                    {
                        Debug.Log(n);
                    }
                }
            }
        }
    }
}
