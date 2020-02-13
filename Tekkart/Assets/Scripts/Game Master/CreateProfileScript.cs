using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateProfileScript : MonoBehaviour
{

    //Keys
    private const string playernamekey = "PLAYER_NAME";
    private const string mastervolume = "MASTER_VOLUME";
    private const string playerpoints = "PLAYER_POINTS";
    private const string racescomplete = "RACES_COMPLETE";
    public InputField nameinput;
    public GameObject ProceedObject;
    public Text ProceedTextBox;

    public GameObject todisable;
    public GameObject toenable;

    public void OnEnable()
    {
        //Volume Set Up
        PlayerPrefs.SetFloat(mastervolume, 0.5f);

        //Player Stats
        PlayerPrefs.SetInt("PLAYER_POINTS", 0);
        PlayerPrefs.SetInt(racescomplete, 0);
        


        //Time Trial Set Up
        //Set up loop with arrays instead?

        //Chaolan Manor
        PlayerPrefs.SetFloat("CMS", 600.00f);
        PlayerPrefs.SetFloat("CMP", 600.00f);
        //Infinite Azure
        PlayerPrefs.SetFloat("IAS", 600.00f);
        PlayerPrefs.SetFloat("IAP", 600.00f);
        //Mt Mishima
        PlayerPrefs.SetFloat("MMS", 600.00f);
        PlayerPrefs.SetFloat("MMP", 600.00f);
        //Wolves Den
        PlayerPrefs.SetFloat("WDS", 600.00f);
        PlayerPrefs.SetFloat("WDP", 600.00f);


    }

    public void AreYouSure()
    {
        ProceedObject.SetActive(true);
        ProceedTextBox.text = "Proceed with the name "+ nameinput.text + "? \n(This name cannot be changed later)";
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
        PlayerPrefs.SetString(playernamekey, nameinput.text);
        todisable.SetActive(false);
        toenable.SetActive(true);
    }
}  
