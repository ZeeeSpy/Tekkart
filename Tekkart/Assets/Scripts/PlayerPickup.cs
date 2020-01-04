using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour, Pickupable
{
    private bool HasPickUp = false;
    private string[] ItemArray;
    private int ItemNumb = -1;

    private void Awake()
    {
        ItemArray = new string[3] { "Boost", "Airstrike", "Trap" };    
    }

    public void GetPickUp()
    {
        if (!HasPickUp)
        {
            int numb = Random.Range(0, 3);

            switch (numb)
            {
                case 0:
                    Debug.Log("Got Boost");
                    break;
                case 1:
                    Debug.Log("Got Airstrike");
                    break;
                case 2:
                    Debug.Log("Got Trap");
                    break;
            }
            ItemNumb = numb;
            HasPickUp = true;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("UseItem") && HasPickUp)
        {
            Debug.Log(ItemArray[ItemNumb]);

            HasPickUp = false;
        }
    }
}
