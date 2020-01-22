using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //TODO replace waitforseconds with actual UI stuff
        yield return new WaitForSeconds(1);
        Debug.Log("Ready");
        yield return new WaitForSeconds(1);
        Debug.Log("Set");
        yield return new WaitForSeconds(1);
        Debug.Log("Go!");

        StartKarts();
    }

    private void StartKarts()
    {
        foreach (Kart actor in Players)
        {
            actor.SetReadyGo();
        }
    }
}
