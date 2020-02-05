using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KartMasterListScript : MonoBehaviour
{
    public GameObject[] AIKarts = new GameObject[9];
    public GameObject[] PlayerKarts = new GameObject[9];
    private GameObject[] GeneratedKartList = new GameObject[9];
    public AudioSource Announcer;
    public AudioClip [] CharacterNameList = new AudioClip[9];
    public GameObject TimeTrialKart;

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

    public void GenerateKartList(int CharacterNumber)
    {
        if (CharacterNumber == -1)
        {
            CharacterNumber = Random.Range(0, 9);
        }

        Announcer.Stop();
        StartCoroutine(CharacterNameRead(CharacterNumber));
        for (int i = 0; i < GeneratedKartList.Length; i++)
        {
            if (i == CharacterNumber)
            {
                GeneratedKartList[i] = PlayerKarts[CharacterNumber];
            }
            else
            {
                GeneratedKartList[i] = AIKarts[i];
            }
        }
    }

    public void GenerateTimeTrialList(int CharacterNumber)
    {
        if (CharacterNumber == -1)
        {
            CharacterNumber = Random.Range(0, 9);
        }

        Announcer.Stop();
        StartCoroutine(CharacterNameRead(CharacterNumber));
        TimeTrialKart = PlayerKarts[CharacterNumber];
    }

    IEnumerator CharacterNameRead(int CharacterNumber)
    {
        yield return new WaitForSeconds(0.3f);
        Announcer.PlayOneShot(CharacterNameList[CharacterNumber]);
    }

    public GameObject[] GetKartList()
    {
        return GeneratedKartList;
    }

    public GameObject GetTimeTrialKart()
    {
        return TimeTrialKart;
    }
}
