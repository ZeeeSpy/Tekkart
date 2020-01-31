using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public AudioSource SFX;
    public AudioClip Highlight;
    public AudioClip Select;

    public AudioSource OST;
    public AudioClip CharacterSelect;
    public AudioClip MenuOST;

    public GameObject ModeSelectObject;
    public GameObject TimeTrialsObject;
    public GameObject GrandPrixObject;
    public GameObject GrandPrixCharacterSelectObject;
    public GameObject ProfileObject;

    public void OnButtonHover()
    {
        SFX.PlayOneShot(Highlight);
    }

    public void OnButtonClick()
    {
        SFX.PlayOneShot(Select);
    }

    public void GrandPrixChar()
    {
        ToggleMenu(GrandPrixCharacterSelectObject);
        OST.Stop();
        OST.clip = CharacterSelect;
        OST.Play();
    }

    public void GrandPrix()
    {
        GrandPrixCharacterSelectObject.SetActive(false);
        GrandPrixObject.SetActive(true);
    }

    public void GrandPrixExit()
    {
        GrandPrixObject.SetActive(false);
        ModeSelectObject.SetActive(true);
        OST.Stop();
        OST.clip = MenuOST;
        OST.Play();
    }

    public void TimeTrials()
    {
        ToggleMenu(TimeTrialsObject);
    }

    public void VSRace()
    {

    }

    public void Battle()
    {

    }

    public void Profile()
    {
        ToggleMenu(ProfileObject);
    }

    public void Info()
    {

    }

    public void Options()
    {

    }

    private void ToggleMenu(GameObject menutotoggle)
    {
        menutotoggle.SetActive(!menutotoggle.activeInHierarchy);
        ModeSelectObject.SetActive(!ModeSelectObject.activeInHierarchy);
    }

    //https://answers.unity.com/questions/1096174/eventsystemsetselectedgameobject-does-not-highligh.html
    //potential fix for menu keyboard navigation
}