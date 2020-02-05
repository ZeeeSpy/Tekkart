using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CountdownGo : MonoBehaviour
{
    private Kart[] Players;
    private AudioSource Announcer;
    public AudioClip[] ttog = new AudioClip[4];

    private void Awake()
    {
        Announcer = GetComponent<AudioSource>();
        GameObject[] KartsList = GameObject.FindGameObjectsWithTag("Kart");
        Players = new Kart[KartsList.Length];
        for (int j = 0; j < KartsList.Length; j++)
        {
            Players[j] = KartsList[j].gameObject.GetComponent<Kart>();
        }

        if (GameObject.FindGameObjectsWithTag("LoadingScreen").Length == 0)
        { //In Editor
            StartCountDown();
        }

    }


    public void StartCountDown()
    {
        StartCoroutine(ReadySetGo());
    }

    IEnumerator ReadySetGo()
    {
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>().enabled = true;
        GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        Announcer.PlayOneShot(ttog[0]);
        yield return new WaitForSeconds(0.9f);
        Announcer.PlayOneShot(ttog[1]);
        yield return new WaitForSeconds(1);
        Announcer.PlayOneShot(ttog[2]);
        yield return new WaitForSeconds(1);
        Announcer.PlayOneShot(ttog[3]);

        StartKarts();
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }

    private void StartKarts()
    {
        foreach (Kart actor in Players)
        {
            actor.SetReadyGo();
        }
       
    }
}
