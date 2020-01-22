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
        StartCoroutine(ReadySetGo());
    }

    IEnumerator ReadySetGo()
    {
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
