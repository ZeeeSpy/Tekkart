using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsScreenScript : MonoBehaviour
{
    public Text TextBox;
    public Image ImageBox;

    private string[] TextArray;

    private void Awake()
    {

        //TODO make this a JSON at some point
        TextArray = new string[]{
            "Drift around corners without losing speed! While turning press and hold the jump key to start drifting. Be sure to counter steer!",
            "If you drift for long enough you will gain a boost when you let go of the jump key. Drift for longer to get a bigger boost!",
            "Ramps not only make you look cool but they give you a boost when you drive over them.",
            "Drive over pick up boxes to get a usable item.",
            "Be careful how you use your boosts! Boost power doesn't combine! If you are currently boosting another boost won't make you boost for longer!",
            "Unguided Missles: A missile shoots straight forward stunning the first player it hits.\n\nBoost: A free instant boost\n\nTrap: A trap you place behind you, stunning any kart that it comes into contact with." 
        };
    }

    public void SetText(int number)
    {
        TextBox.text = TextArray[number];
    }
}