using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageAndPlaceParent : MonoBehaviour
{ 
    private void Awake()
    {
        PlayerPointScript PPS = GameObject.FindGameObjectWithTag("PlayerPoints").GetComponent<PlayerPointScript>();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Text>().text = PPS.GetTrackAndPos(i);
        }
    }
}
