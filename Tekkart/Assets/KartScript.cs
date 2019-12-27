using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KartScript : MonoBehaviour, Kart
{
    public Rigidbody KartSphere;
    public Transform kartNormal;
    public LayerMask layerMask;

    public Animator animator;

    public Text speedometertext;


    float speed, currentSpeed;
    float currentRotate;
    public float rotate;

    //Kart Stats
    public float TopSpeed = 60f;
    public float steering = 15f;
    public float acceleration = 5f;
    public float handling = 4f;
    const float gravity = 10f;

    private bool Boostbool = false;

    const float MaxBoostTime = 1f;
    float CurrentBoostTime = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.position = KartSphere.transform.position - new Vector3(0f, 0.8f, 0);
        //make kart follow sphere

        //Go Forward
        if (Input.GetButton("Fire1"))
        {
            speed = TopSpeed;
            if (Boostbool)
            {
                speed = TopSpeed + TopSpeed * 1.5f;
            }
        }


        //Break
        if (Input.GetButton("Fire3"))
        {
            speed = TopSpeed * -0.25f;
        }


        //Left Right
        if (Input.GetAxis("Horizontal") != 0)
        {
            int dir = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
            float amount = Mathf.Abs((Input.GetAxis("Horizontal")));
            Steer(dir, amount);
            animator.SetFloat("Direction", rotate);
        }
        
        if (Boostbool)
        {
            currentSpeed = Mathf.SmoothStep(currentSpeed, speed, Time.deltaTime * acceleration * 2); speed = 0f;
        } else
        {
            currentSpeed = Mathf.SmoothStep(currentSpeed, speed, Time.deltaTime * acceleration); speed = 0f;
        }


        currentRotate = Mathf.Lerp(currentRotate, rotate, Time.deltaTime * handling); rotate = 0f;

        //Boost Wear Off
        if (Boostbool)
        {
            CurrentBoostTime = CurrentBoostTime - Time.deltaTime;
            if (CurrentBoostTime < 0)
            {
                Boostbool = false;
                CurrentBoostTime = MaxBoostTime;
            }
        }

        int Speedoval = (int)currentSpeed;
        speedometertext.text = Speedoval.ToString();
    }

    private void FixedUpdate()
    {

        //Going Forward
        KartSphere.AddForce(transform.forward * currentSpeed, ForceMode.Acceleration);

        //Gravity
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

    private void Steer(int direction, float amount)
    {
        rotate = (steering * direction) * amount;
    }

    public void Boost()
    {
        Boostbool = true;
        CurrentBoostTime = MaxBoostTime;
    }
}
