using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnYAxis : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(Time.deltaTime * 0f, 0f, 2.5f), Space.Self);
    }
}
