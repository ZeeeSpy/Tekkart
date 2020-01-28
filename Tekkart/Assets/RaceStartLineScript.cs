using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class RaceStartLineScript : MonoBehaviour
{
    private Transform[] StartLinePositionsArray = new Transform[9];

    public GameObject[] KartsToSpawn = new GameObject[9];

    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            StartLinePositionsArray[i] = transform.GetChild(i);
        }

        string[] Order;

        GameObject temp = GameObject.FindGameObjectWithTag("PlayerPoints");
        PlayerPointScript PPS = temp.GetComponent<PlayerPointScript>();
        Order = PPS.GetStartLinePositions();

        if (Order == null)
        {
            UnityEngine.Random R = new UnityEngine.Random();
            reshuffle(KartsToSpawn);
            int i = 0;
            foreach (Transform startposition in StartLinePositionsArray)
            {
                var thiskart = Instantiate(KartsToSpawn[i], StartLinePositionsArray[i].position, Quaternion.identity);
                thiskart.transform.SetParent(transform);
                i++;
            }
        } else
        {
            //Karts on line via Order
            Array.Reverse(Order);
            for (int i = 0; i < Order.Length; i++)
            {
                for (int q = 0; q < KartsToSpawn.Length; q++)
                {
                    string kartname = KartsToSpawn[q].transform.GetChild(1).GetComponent<Kart>().GetName();
                    if (kartname == Order[i].Trim()) //String NEEDS to be trimmed or else a random space will be added at the start?
                    {
                        var SpawnedKart = Instantiate(KartsToSpawn[q], StartLinePositionsArray[i].position, Quaternion.identity);
                        SpawnedKart.transform.SetParent(this.transform);
                        break;
                    }
                }
            }
        }
    }


    void reshuffle(GameObject[] ToShuff)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia 
        //This shuffle courtesy of HarvesteR on the unity forums
        for (int t = 0; t < ToShuff.Length; t++)
        {
            GameObject tmp = ToShuff[t];
            int r = UnityEngine.Random.Range(t, ToShuff.Length);
            ToShuff[t] = ToShuff[r];
            ToShuff[r] = tmp;
        }
    }
}
