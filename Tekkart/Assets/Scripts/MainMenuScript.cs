using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public AudioSource SFX;
    public AudioClip Highlight;
    public AudioClip Select;

    public AudioSource OST;
    public AudioClip CharacterSelect;
    public AudioClip MenuOST;

    public AudioSource Announcer;
    public AudioClip Welcome;

    public GameObject ModeSelectObject;
    public GameObject TimeTrialsObject;
    public GameObject GrandPrixObject;
    public GameObject GrandPrixCharacterSelectObject;
    public GameObject StoryCharacterSelectObject;
    public GameObject ProfileObject;
    public GameObject TimeTrialsCharacterSelectObject;
    public GameObject OptionsObject;

    public void OnButtonHover()
    {
        SFX.PlayOneShot(Highlight);
    }

    public void OnButtonClick()
    {
        SFX.PlayOneShot(Select);
    }

    public void StoryChar()
    {
        CharacterSelectScreen(StoryCharacterSelectObject);
    }

    public void GrandPrixChar()
    {
        CharacterSelectScreen(GrandPrixCharacterSelectObject);
    }

    private void CharacterSelectScreen(GameObject TargetCharScreen)
    {
        //UltraDumbWorkAround To Fix "Pointer Exit" not happening on click
        for (int i = 0; i < TargetCharScreen.transform.childCount; i++)
        {
            GameObject Go = TargetCharScreen.transform.GetChild(i).gameObject;
            if (Go.name == "CharacterGrid")
            {
                for (int j = 0; j < Go.transform.childCount; j++)
                {
                    GameObject Char = Go.transform.GetChild(j).gameObject;
                    for (int q = 0; q < Char.transform.childCount; q++)
                    {
                        GameObject Glow = Char.transform.GetChild(q).gameObject;
                        if (Glow.name == "Glow")
                        {
                            Glow.GetComponent<Image>().enabled = false;
                        }
                    }

                }
            }
            else
            {
                continue;
            }
        }

        ToggleMenu(TargetCharScreen);
        StartCoroutine("CharacterSelectSounds");
    }

    IEnumerator CharacterSelectSounds()
    {
        OST.Stop();
        Announcer.PlayOneShot(Welcome);
        yield return new WaitForSeconds(0.75f);
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

    public void StoryMode(int charnumber)
    {
        string toload = "Story" + charnumber.ToString();
        SceneManager.LoadScene(toload);
    }

    public void TimeTrialsChar()
    {
        CharacterSelectScreen(TimeTrialsCharacterSelectObject);
    }

    public void TimeTrials()
    {
        TimeTrialsCharacterSelectObject.SetActive(false);
        TimeTrialsObject.SetActive(true);
    }

    public void TimeTrialsExit()
    {
        TimeTrialsObject.SetActive(false);
        ModeSelectObject.SetActive(true);
        OST.Stop();
        OST.clip = MenuOST;
        OST.Play();
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
        ToggleMenu(OptionsObject);
    }

    private void ToggleMenu(GameObject menutotoggle)
    {
        menutotoggle.SetActive(!menutotoggle.activeInHierarchy);
        ModeSelectObject.SetActive(!ModeSelectObject.activeInHierarchy);
    }

    //https://answers.unity.com/questions/1096174/eventsystemsetselectedgameobject-does-not-highligh.html
    //potential fix for menu keyboard navigation
}