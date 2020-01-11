using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normalise : MonoBehaviour
{
    private LayerMask layerMask;
    // Update is called once per frame
    void Update()
    {
        //Making the kart follow the road properly
        RaycastHit hitOn;
        RaycastHit hitNear;

        Physics.Raycast(transform.position + (transform.up * .1f), Vector3.down, out hitOn, 1.1f, layerMask);
        Physics.Raycast(transform.position + (transform.up * .1f), Vector3.down, out hitNear, 2.0f, layerMask);

        //Normal Rotation
        transform.up = Vector3.Lerp(transform.up, hitNear.normal, Time.deltaTime * 8.0f);
        transform.Rotate(0, transform.eulerAngles.y, 0);
    }
}
