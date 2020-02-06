using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeTrialResults : MonoBehaviour
{
    public Text Notification;
    public Text PersonalBest;
    public Text CurrentTime;
    public Text LapTimes;
    public Text SystemBest;
    public Canvas ThisCanvas;
    public GameObject UI;


    public void SetResults(decimal[] LapTimesArr, bool NewPB, string StageName)
    {
        if (NewPB)
        {
            Notification.text = "New PB!";
        } else
        {
            Notification.text = "You're not as tough as you talk!";
        }

        //PB calculations
        float totaltime = PlayerPrefs.GetFloat(StageName + "P");
        int minutes = (int)totaltime / 60;
        decimal seconds = (decimal)totaltime - (minutes * 60);
        PersonalBest.text = ("Personal Best: " + minutes + ":" + seconds);

        //Calculation of current race time and lap times
        string LapTimeString = "";
        totaltime = 0;
        for (int i = 0; i < LapTimesArr.Length; i++)
        {
            Debug.Log("Lap" + i + ": " + LapTimesArr[i]);
            LapTimeString = LapTimeString + "Lap " + i + ":  " + LapTimesArr[i] +"\n";
            totaltime = totaltime + (float)LapTimesArr[i];
        }
        minutes = (int)totaltime / 60;
        seconds = (decimal)totaltime - (minutes * 60);

        CurrentTime.text = ("This Race: " + minutes + ":" + seconds);
        LapTimes.text = LapTimeString;

        //Format Heihachi's time
        totaltime = PlayerPrefs.GetFloat(StageName + "S");
        Debug.Log(totaltime);
        minutes = (int)totaltime / 60;
        seconds = (decimal)totaltime - (minutes * 60);
        SystemBest.text = (minutes + ":" + seconds);

        ThisCanvas.enabled = true;
        UI.SetActive(false);
    }


    public void Retry()
    {
        GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreenScript>().ShowLoadingScreen(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreenScript>().ShowLoadingScreen("PressStart");
    }
}
