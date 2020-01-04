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

    private void Awake()
    {
        ItemArray = new string[3] { "Boost", "Airstrike", "Trap" };
        ThisKart = GetComponent<KartScript>();
        ItemList = GameObject.FindGameObjectWithTag("ItemParent").GetComponent<ItemParent>();
    }

    public void GetPickUp()
    {
        if (!HasPickUp)
        {
            int numb = Random.Range(0, 3);

            switch (numb)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
            ItemNumb = numb;
            HasPickUp = true;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("UseItem") && HasPickUp)
            switch (ItemNumb)
            {
                case 0:
                    ThisKart.Boost();
                    break;
                case 1:
                    CallInAirStrike();
                    break;
                case 2:
                    Instantiate(ItemList.GetTrap(), transform.position + new Vector3(0, 2, -2.5f), Quaternion.identity);
                    break;
            }
            HasPickUp = false;
    }

    private void CallInAirStrike()
    {

    }
}

