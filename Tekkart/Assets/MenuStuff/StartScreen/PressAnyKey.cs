using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKey : MonoBehaviour
{
    public GameObject toenable;
    public GameObject todisable;
    // Update is called once per frame
    public AudioSource SFX;
    public AudioSource OST;

    public AudioClip SFXSelect;
    
    void Update()
    {
        if (Input.anyKey)
        {
            SFX.PlayOneShot(SFXSelect);
            OST.Play();
            toenable.SetActive(true);
            todisable.SetActive(false);
        }
    }
}
