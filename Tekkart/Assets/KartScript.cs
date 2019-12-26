using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartScript : MonoBehaviour
{
    public Rigidbody KartSphere;
    public Transform kartNormal;
    public LayerMask layerMask;


    float speed, currentSpeed;
    float rotate, currentRotate;

    private float acceleration = 30f;
    private float steering = 30f;
    private float gravity = 10f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = KartSphere.transform.position - new Vector3(0f,0.8f,0);
        //make kart follow sphere

        if (Input.GetButton("Fire1"))
        {
            speed = acceleration;
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            int dir = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
            float amount = Mathf.Abs((Input.GetAxis("Horizontal")));
            Steer(dir, amount);
        }

        currentSpeed = Mathf.SmoothStep(currentSpeed, speed, Time.deltaTime * 50f); speed = 0f;
        currentRotate = Mathf.Lerp(currentRotate, rotate, Time.deltaTime * 4f); rotate = 0f;
    }

    private void FixedUpdate()
    {
        KartSphere.AddForce(transform.forward * currentSpeed, ForceMode.Acceleration);

        KartSphere.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        //Steering
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + currentRotate, 0), Time.deltaTime * 5f);

        RaycastHit hitOn;
        RaycastHit hitNear;

        Physics.Raycast(transform.position + (transform.up * .1f), Vector3.down, out hitOn, 1.1f, layerMask);
        Physics.Raycast(transform.position + (transform.up * .1f), Vector3.down, out hitNear, 2.0f, layerMask);

        //Normal Rotation
        kartNormal.up = Vector3.Lerp(kartNormal.up, hitNear.normal, Time.deltaTime * 8.0f);
        kartNormal.Rotate(0, transform.eulerAngles.y, 0);

    }

    public void Steer(int direction, float amount)
    {
        rotate = (steering * direction) * amount;
    }
}
