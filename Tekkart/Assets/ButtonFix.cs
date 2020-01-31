using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonFix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(es.firstSelectedGameObject);
    }
}
