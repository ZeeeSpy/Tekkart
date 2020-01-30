using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartMasterListScript : MonoBehaviour
{
    public GameObject[] AIKarts = new GameObject[9];
    public GameObject[] PlayerKarts = new GameObject[9];
    private GameObject[] GeneratedKartList = new GameObject[9];
    private bool KartsGenerated = false;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("KartMasterList");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }


    public GameObject[] GetKartList(int CharacterNumber)
    {
        if (!KartsGenerated)
        {
            //TODO Generate Karts and put in the correct player kart
            KartsGenerated = true;
        }

        return GeneratedKartList;
    }
}
