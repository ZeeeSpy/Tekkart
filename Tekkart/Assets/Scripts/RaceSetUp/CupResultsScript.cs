using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CupResultsScript : MonoBehaviour
{
    public void Continue()
    {
        Destroy(GameObject.FindGameObjectWithTag("PlayerPoints"));
        Destroy(GameObject.FindGameObjectWithTag("KartMasterList"));
        //DestroyUneeded Don'tDestroyOnLoad When going back to the menu.

        GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreenScript>().ShowLoadingScreen("PressStart");
    }
}
