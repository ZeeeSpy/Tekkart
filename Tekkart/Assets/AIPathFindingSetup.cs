using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPathFindingSetup : MonoBehaviour
{
    public float MinDistance = 0;
    public float RandomRange = 0;
    private void Awake()
    {
        GameObject[] KartsList = GameObject.FindGameObjectsWithTag("Kart");
        AiKart[] AiKarts = new AiKart[KartsList.Length -1];
        int i = 0;
        foreach (GameObject s in KartsList)
        {
            if (s.GetComponent<Kart>().GetName() == "Player")
            {
                continue;
            } else
            {
                AiKarts[i] = s.GetComponent<AiKart>();
                i++;
            }
        }
        
        foreach (AiKart KartToSetUp in AiKarts)
        {
            KartToSetUp.SetUpAIPathing(RandomRange, MinDistance);
        }
    }
}