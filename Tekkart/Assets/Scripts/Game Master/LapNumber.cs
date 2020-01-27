using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using System.Text.RegularExpressions;

public class LapNumber : MonoBehaviour, LapManager
{
    public Text LapNumberUI;
    public Text PositionDebug;
    public int LapLength = 3;

    private int NumberOfCheckpoints = -1;
    private Kart[] Players;
    private Kart PCKart;
    private List<Kart> KartsToCheck = new List<Kart>();
    private Dictionary<string, float> PlayerPositions = new Dictionary<string, float>();
    private Vector3[] CheckPointLocations;

    private string[] FinalPositions;
    private int FinalPositionCount = 0;
    private bool racefinished = false;

    public KartScript PlayerKartScript;
    public PlayerPointScript PlayerPoints;

    private int FrameCount = 0;

    public string SceneToLoad = "PressStart";

    private void Awake()
    {
        try
        {
            PlayerPoints = GameObject.FindGameObjectWithTag("PlayerPoints").GetComponent<PlayerPointScript>();
        }
        catch
        {
            Debug.Log("Level Played Out Of Order");
        }

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

        FinalPositions = new string [KartsList.Length];
        for (int k = 0; k < FinalPositions.Length; k++)
        {
            FinalPositions[k] = "NA";
        }
    }

    public void CheckIn(int inccheckpoint, Kart ThisKart)
    {
        if (!racefinished)
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
                        if (ThisKart.GetCheckPointValue() >= LapLength + 1)
                        {
                            FinalPositions[FinalPositionCount] = ThisKart.GetName();
                            FinalPositionCount = FinalPositionCount + 1;
                            EndRace();
                        }
                    }

                    if (ThisKart.GetCheckPointValue() >= LapLength + 1 && ThisKart.GetName() != "Player")
                    {
                        FinalPositions[FinalPositionCount] = ThisKart.GetName();
                        FinalPositionCount = FinalPositionCount + 1;
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
    }

    private void Update()
    {
        FrameCount++;
        if (FrameCount == 10)
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
                if (PCKart.GetTargetCheckPoint() == NumberOfCheckpoints - 1)
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
                }
                if (relativeoffset == -5)
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
            FrameCount = 0;
        }
    }

    private void EndRace()
    {
        racefinished = true;
        string[] order = PositionSnapShot();
        int numberoffinish = -1;
        if (FinalPositions[0] == "Player") //Player is first
        {
            //position snapshot should just work?
        }
        else if (FinalPositions[FinalPositions.Length - 1] == "Player")
        { //player is last no need to do a snapshot
            string outputa = "";
            foreach (string position in FinalPositions)
            {
                outputa = outputa + position + " : ";
            }
            Debug.Log(outputa);


            try
            {
                PlayerPoints.UpdateScores(FinalPositions, SceneToLoad);
            }
            catch
            {
                Debug.Log("No Point Manager Found, No Level Will Be Loaded");
            }

            //Camera Stuff
            GameObject[] CamerasToDeactT = GameObject.FindGameObjectsWithTag("CameraD");

            foreach (GameObject CO in CamerasToDeactT)
            {
                CO.SetActive(false);
            }

            GameObject.FindGameObjectWithTag("CameraA").GetComponent<Camera>().enabled = true;
            return;
        }
        else //Player isn't first
        {
            
            for (int i = 0; i < FinalPositions.Length; i++)
            {
                if (FinalPositions[i] == "NA")
                {
                    numberoffinish = i;
                    break;
                } 
            }
        }

        if (numberoffinish == -1) { numberoffinish = 1; }

        for (int j = numberoffinish; j < FinalPositions.Length; j++)
        {
            FinalPositions[j] = order[j];
        }

        string[] RaceFinishPosition = new string[FinalPositions.Length];

        for (int q = 0; q < FinalPositions.Length; q++)
        {
            RaceFinishPosition[q] = Regex.Replace(FinalPositions[q], "[0-9]", "");
        }

        try
        {
            PlayerPoints.UpdateScores(RaceFinishPosition, SceneToLoad);
        }
        catch
        {
            Debug.Log("No Point Manager Found, No Level Will Be Loaded");
        }



        //Camera Stuff
        GameObject[] CamerasToDeact = GameObject.FindGameObjectsWithTag("CameraD");

        foreach (GameObject CO in CamerasToDeact)
        {
            CO.SetActive(false);
        }
        GameObject.FindGameObjectWithTag("CameraA").GetComponent<Camera>().enabled = true;
    }

    private string[] PositionSnapShot()
    {
        string[] outputA = new string[Players.Length];
        for (int i = 0; i < Players.Length; i++)
        {
            outputA[i] = Players[i].GetCheckPointValue() + " " + Players[i].GetName();
        }

        string[] outputB = outputA.OrderBy(x => x).ToArray();
        Array.Reverse(outputB);

        for (int j = 0; j < outputB.Length; j++)
        {
            outputB[j] = Regex.Replace(outputB[j], "[^A-Za-z0-9 _]", "");
        }
        return outputB;
    }
}
