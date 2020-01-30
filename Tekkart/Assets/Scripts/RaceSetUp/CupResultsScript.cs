using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CupResultsScript : MonoBehaviour
{
    public void Continue()
    {
        GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreenScript>().ShowLoadingScreen("PressStart");
    }
}
