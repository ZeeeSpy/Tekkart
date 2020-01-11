using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour, Pickupable
{
    private bool HasPickUp = false;
    private string[] ItemArray;
    private int ItemNumb = -1;
    private KartScript ThisKart;
    private ItemParent ItemList;
    public GameObject Normal;

    private void Awake()
    {
        ItemArray = new string[4] { "Boost", "Airstrike", "Trap", "UnguidedMissile" };
        ThisKart = GetComponent<KartScript>();
        ItemList = GameObject.FindGameObjectWithTag("ItemParent").GetComponent<ItemParent>();
    }

    public void GetPickUp()
    {
        if (!HasPickUp)
        {
            int numb = Random.Range(0, 4);
            
            Debug.Log("Got: " + ItemArray[numb]);
            //TODO update ui 
            /*
            switch (numb)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
            */
            ItemNumb = numb;
            HasPickUp = true;

            //Debug :)
            //ItemNumb = 3;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("UseItem") && HasPickUp)
        { 
            Debug.Log("Item used");
            HasPickUp = false;
            switch (ItemNumb)
            {
                case 0:
                    ThisKart.Boost();
                    break;
                case 1:
                    CallInAirStrike();
                    break;
                case 2:
                    Instantiate(ItemList.GetTrap(), transform.position + new Vector3(0, 2, -2.5f), Normal.transform.rotation);
                    break;
                case 3:
                    Vector3 rot = Normal.transform.eulerAngles;
                    rot = new Vector3(rot.x *-1, rot.y + 180, rot.z);
                    var MissileRot = Quaternion.Euler(rot);
                    Instantiate(ItemList.GetUnguidedMissile(), transform.position + new Vector3(0, 1, 2), MissileRot);
                    break;
            }
        }
    }

    private void CallInAirStrike()
    {
        /*
         * attack only first place
         * 
         * 
         * 
         */
    }
}

