using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnYAxisReal : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(Time.deltaTime * 0f, 0.5f, 0f), Space.Self);
    }
}
