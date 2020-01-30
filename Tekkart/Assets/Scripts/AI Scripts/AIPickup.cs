using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPickup : MonoBehaviour, Pickupable
{
    private bool HasPickUp = false;
    private int ItemNumb = -1;
    private AIScript ThisKart;
    private ItemParent ItemList;
    public GameObject Normal;
    private bool CoroutineRunning = false;
    public GameObject Sphere;

    private void Awake()
    {
        ThisKart = GetComponent<AIScript>();
        ItemList = GameObject.FindGameObjectWithTag("ItemParent").GetComponent<ItemParent>();
    }

    public void GetPickUp()
    {
        if (!HasPickUp)
        {
            int numb = Random.Range(0, 3);
            ItemNumb = numb;
            HasPickUp = true;
        }
    }

    private void Update()
    {
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
                    Ray ray = new Ray(Sphere.transform.position + new Vector3(0, 2, 0), Normal.transform.forward);
                    //Debug.DrawRay(Normal.transform.position + new Vector3(0, 2, 0), Normal.transform.forward * 75, Color.green);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 70f))
                    {
                        if (hit.transform.parent != null)
                        {
                            if (hit.transform.parent.tag == "Characters")
                            {
                                StartCoroutine("UnguidedMissile");
                            }
                        }
                    }
                    break;
            }
        }
    }

    IEnumerator TrapCoroutine()
    {
        CoroutineRunning = true;
        yield return new WaitForSeconds(Random.Range(5, 16));
        Instantiate(ItemList.GetTrap(), (Sphere.transform.position - transform.forward * 4f), Normal.transform.rotation);
        CoroutineRunning = true;
        HasPickUp = false;
    }

    IEnumerator UnguidedMissile()
    {
        CoroutineRunning = true;
        yield return new WaitForSeconds(Random.Range(0.1f, 2.5f));
        Vector3 rot = Normal.transform.eulerAngles;
        rot = new Vector3(rot.x * -1, rot.y + 180, rot.z);
        var MissileRot = Quaternion.Euler(rot);
        Instantiate(ItemList.GetUnguidedMissile(), (Sphere.transform.position + transform.forward * 2f), MissileRot);
        CoroutineRunning = true;
        HasPickUp = false;
    }
}


