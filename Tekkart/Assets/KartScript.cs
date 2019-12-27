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
    public Text debug;
    public Text driftdebug;

    public ParticleSystem boostparticles;
    public ParticleSystem boostparticles2;

    float speed, currentSpeed;
    float currentRotate;
    public float rotate;

    //Kart Stats
    public float TopSpeed = 60f;
    public float steering = 15f;
    public float acceleration = 5f;
    public float handling = 4f;

    //Non Kart Stats
    private bool drifting;
    private int driftDirection;
    const float gravity = 10f;
    private float driftPower;
    float amount;

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
            amount = Mathf.Abs((Input.GetAxis("Horizontal")));
            Steer(dir, amount);
            animator.SetFloat("Direction", rotate);
        } else
        {
            amount = 0;
        }



        //Drifting 
        if (Input.GetButtonDown("Jump") && !drifting && Input.GetAxis("Horizontal") != 0)
        {
            drifting = true;
            driftDirection = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
        }

        if (Input.GetButtonUp("Jump") && drifting)
        {
            drifting = false;
        }

        if (drifting)
        {
            float control;
            //right, left
            if (driftDirection == 1)
            {
                driftdebug.text = "Direction: Right";
                control = 0.5f + (Input.GetAxis("Horizontal")*0.4f);
            } else
            {
                driftdebug.text = "Direction: Left";
                control = 0.5f + (Input.GetAxis("Horizontal") * -0.4f);
            }

            driftdebug.text = driftdebug.text + "\n Control: " + control.ToString();

            debug.text = "Drifting: True";
            Steer(driftDirection, control);
        } else
        {
            debug.text = "Drifting: False";
        }


        //Rotate and go forward
        if (Boostbool)
        {
            currentSpeed = Mathf.SmoothStep(currentSpeed, speed, Time.deltaTime * acceleration * 2); speed = 0f;
        } else
        {
            currentSpeed = Mathf.SmoothStep(currentSpeed, speed, Time.deltaTime * acceleration); speed = 0f;
        }
        currentRotate = Mathf.Lerp(currentRotate, rotate, Time.deltaTime * handling); rotate = 0f;



        //Boost 
        if (Boostbool)
        {
            debug.text = debug.text + "\n Boost: True";
            var main = boostparticles.main;
            var main2 = boostparticles2.main;
            main.startLifetime = 2.5f;
            main2.startLifetime = 2.5f;
            CurrentBoostTime = CurrentBoostTime - Time.deltaTime;
            if (CurrentBoostTime < 0)
            {
                main.startLifetime = 0;
                main2.startLifetime = 0;
                Boostbool = false;
                CurrentBoostTime = MaxBoostTime;
            }
        } else
        {
            debug.text = debug.text + "\n Boost: False";
        }

        //Hud Stuff
        int Speedoval = (int)currentSpeed;
        if (Speedoval < 0) { Speedoval = Speedoval * -1; }
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

        //Making the kart follow the road properly
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
