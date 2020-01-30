using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParent : MonoBehaviour
{
    public GameObject AirStrike;
    public GameObject Trap;
    public GameObject UnguidedMissile;


    public GameObject GetAirStrike()
    {
        return AirStrike;
    }

    public GameObject GetTrap()
    {
        return Trap;
    }

    public GameObject GetUnguidedMissile()
    {
        return UnguidedMissile;
    }
}
