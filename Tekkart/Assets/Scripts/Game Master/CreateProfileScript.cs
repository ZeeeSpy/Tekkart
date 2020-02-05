using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateProfileScript : MonoBehaviour
{

    //Keys
    private const string playernamekey = "PLAYER_NAME";
    private const string playerscore = "PLAYER_POINTS";
    private const string mastervolume = "MASTER_VOLUME";
    public InputField nameinput;
    public GameObject ProceedObject;
    public Text ProceedTextBox;

    public GameObject todisable;
    public GameObject toenable;

    public void OnEnable()
    {
        PlayerPrefs.SetFloat(mastervolume, 0.5f);
    }

    public void AreYouSure()
    {
        ProceedObject.SetActive(true);
        ProceedTextBox.text = "Proceed with the name "+ nameinput.text + "? \n(This name can be changed later)";
    }


    public void No()
    {
        nameinput.text = "";
        ProceedObject.SetActive(false);
    }

    public void Yes()
    {
        //Set up profile here
        //Player name
        //Favorite Character
        //Time Trial Records
        //Etc etc
        PlayerPrefs.SetInt(playerscore, 1000);
        PlayerPrefs.SetString(playernamekey, nameinput.text);
        todisable.SetActive(false);
        toenable.SetActive(true);
    }
}  
