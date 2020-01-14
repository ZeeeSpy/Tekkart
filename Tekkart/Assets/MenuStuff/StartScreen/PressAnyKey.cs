using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKey : MonoBehaviour
{
    public GameObject toenable;
    public GameObject todisable;
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            toenable.SetActive(true);
            todisable.SetActive(false);
        }
    }
}
