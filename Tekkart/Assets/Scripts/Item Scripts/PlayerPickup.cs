using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickup : MonoBehaviour, Pickupable
{
    private bool HasPickUp = false;
    private string[] ItemArray;
    private int ItemNumb = -1;
    private KartScript ThisKart;
    private ItemParent ItemList;
    public GameObject Normal;
    public Transform Sphere;
    private Text ItemUI;

    private void Awake()
    {
        ItemArray = new string[3] { "Boost", "Trap", "UnguidedMissile" };
        ThisKart = GetComponent<KartScript>();
        try
        {
            ItemList = GameObject.FindGameObjectWithTag("ItemParent").GetComponent<ItemParent>();
            ItemUI = GameObject.FindGameObjectWithTag("CurrentItemText").GetComponent<Text>();
        } catch
        {
            //In time trial
        }
    }

    public void GetPickUp()
    {
        if (!HasPickUp)
        {
            int numb = Random.Range(0, 3);
            
            Debug.Log("Got: " + ItemArray[numb]);
            ItemUI.text = ItemArray[numb];
            ItemNumb = numb;
            HasPickUp = true;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("UseItem") && HasPickUp)
        { 
            Debug.Log("Item used");
            ItemUI.text = "";
            HasPickUp = false;
            switch (ItemNumb)
            {
                case 0:
                    ThisKart.Boost();
                    break;
                case 1:
                    Instantiate(ItemList.GetTrap(), (Sphere.transform.position - transform.forward * 4f), Normal.transform.rotation);
                    break;
                case 2:
                    Vector3 rot = Normal.transform.eulerAngles;
                    rot = new Vector3(rot.x *-1, rot.y + 180, rot.z);
                    var MissileRot = Quaternion.Euler(rot);
                    Instantiate(ItemList.GetUnguidedMissile(), (Sphere.transform.position + transform.forward * 2f), MissileRot);
                    break;
            }
        }
    }

    private void CallInAirStrike()
    {
        /*
         * attack only first place/Select who to target
         */
    }
}

