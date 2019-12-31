using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LapNumber : MonoBehaviour
{
    public Text LapNumberUI;
    public Text PositionDebug;
    public int LapLength = 100;

    private int NumberOfCheckpoints = -1;
    private Kart[] Players;
    private Kart PCKart;
    private List<Kart> KartsToCheck = new List<Kart>();
    private Dictionary<string, float> PlayerPositions = new Dictionary<string, float>();
    private Vector3[] CheckPointLocations;

    private void Awake()
    {
        GameObject[] KartsList = GameObject.FindGameObjectsWithTag("Kart");
        Players = new Kart[KartsList.Length];
        for (int j = 0; j < KartsList.Length; j++)
        {
            Players[j] = KartsList[j].gameObject.GetComponent<Kart>();
        }

        foreach (Kart p in Players)
        {
            PlayerPositions.Add(p.GetName(), 0);
            if (p.GetName() == "Player")
            {
                PCKart = p;
            }
        }

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





    private void Update()
    {
        List<float> CPValueList = new List<float>();
        KartsToCheck.Clear();
        float PCValue = PCKart.GetCheckPointValue();


        //Go through all players and get CPValues
        for (int i = 0; i < Players.Length; i++)
        {
            CPValueList.Add(Players[i].GetCheckPointValue());
            if (Players[i].GetCheckPointValue() == PCValue)
            {
                if (Players[i].GetName() != PCKart.GetName())
                {
                    //If CPValue is same as player but not player add to KartsToCheck
                    KartsToCheck.Add(Players[i]);
                }
            }
        }

        CPValueList.Sort();
        CPValueList.Reverse();
        //Sort CPValue list and reverse it so it's decending
     


        if (KartsToCheck.Count > 0) //If there are Karts that have same CPValue as Player
            //This part doesn't work
        {
            //Find out the Offset clump of like values
            //Eg. Say CPValue list is (0.9, 0.6 , 0.5 , 0.5 , 0.5)
            //Then the offset for like values is 2 (index 1 + 1).
            int offset = 0;
            for (int p = 0; p < CPValueList.Count; p++)
            {
                if (CPValueList[p] == PCValue)
                {
                    offset = p + 1;
                    break;
                }
            }

            float[] Distance = new float[KartsToCheck.Count];
            float PlayerDistance = -100;

            //Find Distances
            //If at last checkpoint, check distance between karts and firstcheckpoint
            if (PCKart.GetTargetCheckPoint() == NumberOfCheckpoints-1)
            {
                for (int i = 0; i < KartsToCheck.Count; i++)
                {
                    Kart CurrentKart = KartsToCheck[i];
                    Distance[i] = Vector3.Distance(CurrentKart.GetPosition(), CheckPointLocations[0]);
                }
                PlayerDistance = Vector3.Distance(PCKart.GetPosition(), CheckPointLocations[0]);
            }

            else //If not at last checkpoint, check distance between karts and Targetcheckpoint of playerKart
            {
                for (int i = 0; i < KartsToCheck.Count; i++)
                {
                    Kart CurrentKart = KartsToCheck[i];
                    Distance[i] = Vector3.Distance(CurrentKart.GetPosition(), CheckPointLocations[PCKart.GetTargetCheckPoint()]);
                }
                PlayerDistance = Vector3.Distance(PCKart.GetPosition(), CheckPointLocations[PCKart.GetTargetCheckPoint()]);
            }

            //Sort distance array of other karts
            System.Array.Sort(Distance);
            

            //Find out players distance relative to other Karts
            int relativeoffset = -5;

            for (int o = 0; o < Distance.Length; o++)
            {
                if (PlayerDistance < Distance[o])
                {
                    relativeoffset = o;
                    break;
                }
            } if (relativeoffset == -5)
            {
                //If players distance is larger than all distances it's last in the clump
                relativeoffset = Distance.Length;
            }


            int PostCalculationPlayerPosition = offset + relativeoffset;
            //Display position on screen;

            PositionDebug.text = PostCalculationPlayerPosition.ToString();
        }
        else //There are no Karts with same CP value as player. 
        { //BEST CASE SCENARIO (works)
            for (int p = 0; p < CPValueList.Count; p++)
            {
                if (CPValueList[p] == PCValue)
                {
                    int outval = p + 1;
                    //PositionDebug.text = outval.ToString();
                }
            }
        }
    }

}
