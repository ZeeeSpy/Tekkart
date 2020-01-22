using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CountdownGo : MonoBehaviour
{
    private Kart[] Players;

    private void Awake()
    {
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
        GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(3);
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
