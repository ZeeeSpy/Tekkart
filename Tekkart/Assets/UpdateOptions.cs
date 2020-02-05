using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateOptions : MonoBehaviour
{
    public Slider MasterVolume;


    private void OnEnable()
    {
        MasterVolume.value = PlayerPrefs.GetFloat("MASTER_VOLUME");
    }
}
