using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryModeIntroScript : MonoBehaviour
{
    public AudioSource Announcer;
    public Image Art;
    public Text StoryTextUI;


    private int CurrentPart = -1;
    public int MaxPart = 0;
    public Sprite[] ArtArray = new Sprite[3];
    public AudioClip[] AnnouncerVoice = new AudioClip[3];
    [TextArea(5, 10)]
    public string[] StoryText = new string[3];


    private void Awake()
    {
        StartCoroutine(StoryCoroutine());
    }

    IEnumerator StoryCoroutine()
    {
        CurrentPart++;
        if (CurrentPart == MaxPart)
        {
            //TODO randomize cup
            GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreenScript>().ShowLoadingScreen("ChaolanManor");
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            Announcer.PlayOneShot(AnnouncerVoice[CurrentPart]);
            Art.sprite = ArtArray[CurrentPart];
            StoryTextUI.text = StoryText[CurrentPart];
            yield return new WaitForSeconds(AnnouncerVoice[CurrentPart].length + 0.5f);
            StartCoroutine(StoryCoroutine());
        }
    }
}
