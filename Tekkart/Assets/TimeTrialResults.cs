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


    public void SetResults(decimal[] LapTimesArr, string StageName, decimal trtt)
    {
        //PB calculations
        float totaltime = PlayerPrefs.GetFloat(StageName + "P");
        int minutes = 0;
        decimal seconds = 0;


        if ((float)trtt < totaltime)
        {
            Notification.text = "New PB!";
            PlayerPrefs.SetFloat(StageName + "P", (float)trtt);
            minutes = (int)trtt / 60;
            seconds = (decimal)trtt - (minutes * 60);
            PersonalBest.text = ("Personal Best: " + minutes + ":" + seconds);
        }
        else
        {
            Notification.text = "You're not as tough as you talk!";
            minutes = (int)totaltime / 60;
            seconds = (decimal)totaltime - (minutes * 60);
            PersonalBest.text = ("Personal Best: " + minutes + ":" + seconds);
        }

        //Calculation of current race time and lap times
        string LapTimeString = "";
        totaltime = 0;
        for (int i = 0; i < LapTimesArr.Length; i++)
        {
            int j = i + 1;
            LapTimeString = LapTimeString + "Lap " + j + ":  " + LapTimesArr[i] +"\n";
            totaltime = totaltime + (float)LapTimesArr[i];
        }
        minutes = (int)totaltime / 60;
        seconds = (decimal)totaltime - (minutes * 60);

        CurrentTime.text = ("This Race: " + minutes + ":" + seconds);
        LapTimes.text = LapTimeString;

        //Format Heihachi's time
        totaltime = PlayerPrefs.GetFloat(StageName + "S");
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
