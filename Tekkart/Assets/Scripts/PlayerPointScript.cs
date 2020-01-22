using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPointScript : MonoBehaviour
{
    private string[,] PlayerPoints;
    private bool firstrace = true;
    private int[] Scores = new int[] { 15, 12, 10, 9, 8, 7, 6, 5, 4 };

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void UpdateScores(string[] PlayerPositions, string SceneName)
    {
        if (firstrace)
        {
            firstrace = false;
            PlayerPoints = new string [PlayerPositions.Length, 2];
            //Add entries to PlayerPoints
            for (int i = 0; i < PlayerPositions.Length; i++)
            {
                PlayerPoints[i, 0] = PlayerPositions[i];
                PlayerPoints[i, 1] = Scores[i].ToString();
            }
        } else
        {
            for (int i = 0; i < PlayerPositions.Length; i++) {
                
                for (int j = 0; j < PlayerPoints.GetLength(0); j++)
                {
                    if (PlayerPositions[i] == PlayerPoints[j, 0])
                    {
                        PlayerPoints[j, 1] = (int.Parse(PlayerPoints[j, 1]) + Scores[i]).ToString();
                        break;
                    }
                }
            }
        }
        StartCoroutine(NextLevel(SceneName));
    }

    IEnumerator NextLevel(string SceneName)
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneName);
    }

    /*
     * Position : Score
     * 
     * 1: 15
     * 2: 12
     * 3: 10
     * 4: 9
     * 5: 8
     * 6: 7
     * 7: 6
     * 8: 5
     * 9: 4
     */
}
