using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartMasterListScript : MonoBehaviour
{
    public GameObject[] AIKarts = new GameObject[9];
    public GameObject[] PlayerKarts = new GameObject[9];
    private GameObject[] GeneratedKartList = new GameObject[9];
    public AudioSource SFX;
    public AudioClip [] CharacterNameList = new AudioClip[9];

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
        SFX.PlayOneShot(CharacterNameList[CharacterNumber]);
        if (CharacterNumber == -1)
        {
            CharacterNumber = Random.Range(0, 9);
        }

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


    public GameObject[] GetKartList()
    {
        return GeneratedKartList;
    }
}
