using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour
{
    //Kart Stuff
    float speed, currentSpeed;
    float currentRotate;
    public float rotate;


    public float TopSpeed = 60f;
    public float steering = 15f;
    public float acceleration = 5f;
    public float handling = 4f;
    public float driftingability = 0.4f;

    const float gravity = 10f;


    public Rigidbody KartSphere;
    public Transform kartNormal;
    public LayerMask layerMask;


    //AI Stuff
    private int NumberOfCheckpoints;
    private int TargetCheckpoint = 1;
    public GameObject CheckPointParent;
    private Transform[] CheckpointLocationArray;
    private float AngleToTarget;

    private void Awake()
    {
        CheckpointLocationArray = CheckPointParent.GetComponentsInChildren<Transform>();
        NumberOfCheckpoints = CheckpointLocationArray.Length;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = KartSphere.transform.position - new Vector3(0f, 0.8f, 0);


        //Go Foward
        speed = TopSpeed;

        //Steering
        Vector3 TargetDirection = CheckpointLocationArray[TargetCheckpoint].position - transform.position;
        AngleToTarget = Vector3.SignedAngle(TargetDirection, transform.forward,Vector3.up);
        int dir = AngleToTarget > 0 ? -1 : 1;
        Debug.DrawLine(transform.position, CheckpointLocationArray[TargetCheckpoint].position);
        if(AngleToTarget < 0){AngleToTarget = AngleToTarget * -1;}
        float amount = AngleToTarget / 90;
        if (amount > 1) { amount = 1; };
        Steer(dir, amount);


        currentSpeed = Mathf.SmoothStep(currentSpeed, speed, Time.deltaTime * acceleration * 2); speed = 0f;
        currentRotate = Mathf.Lerp(currentRotate, rotate, Time.deltaTime * handling); rotate = 0f;
    }

    private void FixedUpdate()
    {
        //Going Forward
        KartSphere.AddForce(transform.forward * currentSpeed, ForceMode.Acceleration);

        //Gravity
        KartSphere.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        //Steering
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + currentRotate, 0), Time.deltaTime * 5f);


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
        if (TargetCheckpoint == NumberOfCheckpoints-1)
        {
            TargetCheckpoint = 0;
        }
    }

    private void Steer(int direction, float amount)
    {
        rotate = (steering * direction) * amount;
    }
}
