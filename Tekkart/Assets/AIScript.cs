using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour
{

    private int NumberOfCheckpoints;
    private int TargetCheckpoint = 1;

    public Rigidbody KartSphere;
    public Transform kartNormal;
    public LayerMask layerMask;

    public GameObject CheckPointParent;


    private Transform[] CheckpointLocationArray;


    private void Awake()
    {
        CheckpointLocationArray = CheckPointParent.GetComponentsInChildren<Transform>();
        NumberOfCheckpoints = CheckpointLocationArray.Length;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = KartSphere.transform.position - new Vector3(0f, 0.8f, 0);
    }

    private void FixedUpdate()
    {
        //Making the kart follow the road properly
        RaycastHit hitOn;
        RaycastHit hitNear;

        Physics.Raycast(transform.position + (transform.up * .1f), Vector3.down, out hitOn, 1.1f, layerMask);
        Physics.Raycast(transform.position + (transform.up * .1f), Vector3.down, out hitNear, 2.0f, layerMask);

        //Normal Rotation
        kartNormal.up = Vector3.Lerp(kartNormal.up, hitNear.normal, Time.deltaTime * 8.0f);
        kartNormal.Rotate(0, transform.eulerAngles.y, 0);
    }

    public void CheckPointReached() {
        TargetCheckpoint++;
        if (TargetCheckpoint == NumberOfCheckpoints)
        {
            TargetCheckpoint = 0;
        }
    }
}
