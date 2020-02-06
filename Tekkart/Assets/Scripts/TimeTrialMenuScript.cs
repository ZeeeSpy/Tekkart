using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeTrialMenuScript : MonoBehaviour
{
    public void ChaolanManor()
    {
        GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreenScript>().ShowLoadingScreen("ChaolanManorTT");
    }

    public void InfiniteAzure()
    {
        GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreenScript>().ShowLoadingScreen("InfiniteAzureTT");
    }
}
