﻿using UnityEngine;
using UnityEngine.UI;

public class PositionBarScript : MonoBehaviour
{
    public Text Position;
    public Text PlayerName;
    public Text Points;
    public Image PlayerIcon;

    public void SetUpBar(string Position, string Name, string Points)
    {
        this.Position.text = Position;
        if (Name == "Player")
        {
            PlayerName.text = PlayerPrefs.GetString("PLAYER_NAME");
        }
        else
        {
            PlayerName.text = Name;
        }
        this.Points.text = Points;
        //TODO change Icon depening on character
    }
}
