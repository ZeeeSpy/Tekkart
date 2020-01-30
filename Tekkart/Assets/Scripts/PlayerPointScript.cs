using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerPointScript : MonoBehaviour
{
    private string[,] PlayerPoints;
    private bool firstrace = true;
    private int[] Scores = new int[] { 15, 12, 10, 9, 8, 7, 6, 5, 4 };
    private int[] RacePositionArray = new int[] {-1, -1, -1, -1};
    private string[] StageNameArray = new string[] { "temp","temp","temp","temp"};
     /*
     * Position : Score
     * 
     * 1: 15
     * 2: 12
     * 3: 10
     * 4: 9
     * 5: 8
     * 6: 7
     * 7: 6
     * 8: 5
     * 9: 4
     */
    private int racenumber = 0;
    private string SceneName;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("PlayerPoints");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void UpdateScores(string[] PlayerPositions, string SceneName)
    {
        if (firstrace)
        {
            firstrace = false;
            PlayerPoints = new string [PlayerPositions.Length, 2];
            //Add entries to PlayerPoints
            for (int i = 0; i < PlayerPositions.Length; i++)
            {
                PlayerPoints[i, 0] = PlayerPositions[i];
                PlayerPoints[i, 1] = Scores[i].ToString();
            }
        } else
        {
            for (int i = 0; i < PlayerPositions.Length; i++) {
                
                for (int j = 0; j < PlayerPoints.GetLength(0); j++)
                {
                    if (PlayerPositions[i] == PlayerPoints[j, 0])
                    {
                        PlayerPoints[j, 1] = (int.Parse(PlayerPoints[j, 1]) + Scores[i]).ToString();
                        break;
                    }
                }
            }
        }
        NextLevel(SceneName);
    }

    private void NextLevel(string SceneName)
    {
        this.SceneName = SceneName;
        Bubblesort();
        StageNameArray[racenumber] = SceneName;

        for (int q = 8; q < PlayerPoints.GetLength(0); q--)
        {
            if (PlayerPoints[q, 0] == "Player")
            {
                RacePositionArray[racenumber] = ((q-9)*-1);
                break;
            }
        }
        racenumber++;

        GameObject.Find("UI").SetActive(false);
        GameObject.FindGameObjectWithTag("PRP").GetComponent<PositionBarParent>().ChangeBarValues(PlayerPoints, this);
    }

    public string [,] GetResults()
    {
        return PlayerPoints;
    }

    public string GetTrackAndPos(int numb)
    {
        return (StageNameArray[numb] + ":" +RacePositionArray[numb].ToString());
    }

    private void Bubblesort()
    {
        string tempi;
        string temps;

        for (int j = 0; j <= PlayerPoints.GetLength(0)-2; j++)
        {
            for (int q = 0; q <= PlayerPoints.GetLength(0)-2; q++)
            {
                if (int.Parse(PlayerPoints[q, 1]) > int.Parse(PlayerPoints[q + 1, 1]))
                {
                    tempi = PlayerPoints[q + 1, 1];
                    temps = PlayerPoints[q + 1, 0];

                    PlayerPoints[q + 1, 1] = PlayerPoints[q, 1];
                    PlayerPoints[q + 1, 0] = PlayerPoints[q, 0];

                    PlayerPoints[q, 0] = temps;
                    PlayerPoints[q, 1] = tempi;
                }
            }
        }

        /*
        print out playerpoints 
        
        string toprint = "";
        for (int i = 0; i < PlayerPoints.GetLength(0); i++)
        {
            toprint = toprint + PlayerPoints[i, 0] + "," + PlayerPoints[i, 1] + ". ";
        }
        Debug.Log(toprint);
        */
    }

    public void LoadNext()
    {
        try
        {
            GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreenScript>().ShowLoadingScreen(SceneName);
        } catch
        {
            Debug.Log("Level Played Out Of Order");
        }
    }

    public string[] GetStartLinePositions()
    {
        if (firstrace)
        {
            return null;
        }
        else
        {
            string[] ret = new string[PlayerPoints.GetLength(0)];
            for (int i = 0; i < PlayerPoints.GetLength(0); i++)
            {
                ret[i] = PlayerPoints[i, 0];
            }
            return ret;
        }
    }
}
