using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProfileMenuScript : MonoBehaviour
{
    public Text TextBlock;
    public Text playername;

    private void OnEnable()
    {
        string toshow = "";
        toshow = toshow + "Races Played: "+ PlayerPrefs.GetInt("RACES_COMPLETE") +"\n";
        toshow = toshow + "Total Points: " + PlayerPrefs.GetInt("PLAYER_POINTS") + "\n";
        toshow = toshow + "\n";
        toshow = toshow + "Best Times";
        toshow = toshow + "\n \n";
        toshow = toshow + "Rose Cup \n";
        toshow = toshow + "Chaolan Manor:        " + GetMSMS("CMP") +"\n";
        toshow = toshow + "Wolves Den:             " + GetMSMS("WDP") + "\n";
        toshow = toshow + "Infinite Azure:          " + GetMSMS("IAP") + "\n";
        toshow = toshow + "Mt Mishima:              " + GetMSMS("MMP");

        playername.text = PlayerPrefs.GetString("PLAYER_NAME");
        TextBlock.text = toshow;
    }

    private string GetMSMS(string code)
    {
        float trtt = PlayerPrefs.GetFloat(code);
        int minutes = (int)trtt / 60;
        decimal seconds = (decimal)trtt - (minutes * 60);
        string toret = minutes + ":" + seconds;
        return toret;
    }

    public void ClearDate()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
