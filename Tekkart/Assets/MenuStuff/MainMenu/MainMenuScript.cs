using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public AudioSource SFX;
    public AudioClip Highlight;
    public AudioClip Select;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonHover()
    {
        SFX.PlayOneShot(Highlight);
    }

    public void OnButtonClick()
    {
        SFX.PlayOneShot(Select);
    }

    public void GrandPrix()
    {

    }

    public void TimeTrials()
    {

    }

    public void VSRace()
    {

    }

    public void Battle()
    {

    }

    public void Profile()
    {

    }

    public void Info()
    {

    }

    public void Options()
    {

    }
}
