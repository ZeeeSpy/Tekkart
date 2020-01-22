using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandPrixScript : MonoBehaviour
{
    public void RoseCup()
    {
        GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreenScript>().ShowLoadingScreen("ChaolanManor");
    }
}
