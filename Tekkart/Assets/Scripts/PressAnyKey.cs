using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PressAnyKey : MonoBehaviour
{
    public GameObject toenable;
    public GameObject todisable;
    public GameObject FirstPlayScene;
    public AudioSource SFX;
    public AudioSource OST;
    public GameObject CurrentlySelected;

    public AudioClip SFXSelect;
    
    void Update()
    {
        if (Input.anyKey)
        {
            SFX.PlayOneShot(SFXSelect);
            OST.Play();
            if (PlayerPrefs.HasKey("PLAYER_NAME")) { 
                toenable.SetActive(true);
                todisable.SetActive(false);
                Debug.Log("Loading " + PlayerPrefs.GetString("PLAYER_NAME") + "'s Profile");
            } else
            {
                FirstPlayScene.SetActive(true);
                todisable.SetActive(false);
            }
        }
    }
}
