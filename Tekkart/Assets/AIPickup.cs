using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPickup : MonoBehaviour, Pickupable
{
    private bool HasPickUp = false;
    private int ItemNumb = -1;
    private KartScript ThisKart;
    private ItemParent ItemList;
    public GameObject Normal;
    private bool CoroutineRunning = false;

    private void Awake()
    {
        ThisKart = GetComponent<KartScript>();
        ItemList = GameObject.FindGameObjectWithTag("ItemParent").GetComponent<ItemParent>();
    }

    public void GetPickUp()
    {
        if (!HasPickUp)
        {
            int numb = Random.Range(0, 3);
            ItemNumb = numb;
            HasPickUp = true;
            Debug.Log("Got Item:" + numb);
        }
    }

    private void Update()
    {
        Vector3 forward = Normal.transform.TransformDirection(Vector3.forward) * 10 + new Vector3(0,2,0);
        Debug.DrawRay(Normal.transform.position + new Vector3(0, 2, 0), forward, Color.green);
        

        if (HasPickUp && !CoroutineRunning)
        {
            switch (ItemNumb)
            {
                case 0:
                    ThisKart.Boost();
                    HasPickUp = false;
                    break;
                case 1:
                    StartCoroutine("TrapCoroutine");
                    break;
                case 2:

                    /*
                    Ray ray = new Ray(transform.position, transform.forward);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 30f))
                    {
                        if (hit.transform.tag == "Characters")
                        {
                            //Has LOS
                            Vector3 rot = Normal.transform.eulerAngles;
                            rot = new Vector3(rot.x * -1, rot.y + 180, rot.z);
                            var MissileRot = Quaternion.Euler(rot);
                            Instantiate(ItemList.GetUnguidedMissile(), (transform.position + transform.forward * 2f), MissileRot);
                        }
                    }
                    */

                    break;
            }
        }
    }

    IEnumerator TrapCoroutine()
    {
        CoroutineRunning = true;
        yield return new WaitForSeconds(Random.Range(5, 16));
        Instantiate(ItemList.GetTrap(), (transform.position - transform.forward * 2f), Normal.transform.rotation);
        CoroutineRunning = true;
        HasPickUp = false;
    }
}


