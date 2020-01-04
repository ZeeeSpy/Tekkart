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

    public GameObject BoostParticleParent;
    private ParticleSystem[] BoostParticles;

    float speed, currentSpeed;
    float currentRotate;
    public float rotate;
    private bool currentlyboosting = false;

    //Kart Stats
    public string Name;
    public float TopSpeed = 60f;
    public float steering = 15f;
    public float acceleration = 5f;
    public float handling = 4f;
    public float driftingability = 0.4f;

    //Non Kart Stats
    private bool drifting;
    private int driftDirection;
    const float gravity = 10f;
    private float driftPower;
    float amount;
    private bool Stunned = false;

    [Header("CheckPoint Logic")]
    [SerializeField]
    private int TargetCheckPoint = 0;
    [SerializeField]
    private int CurrentCheckPoint = -1;
    [SerializeField]
    private float CheckPointValue = 1;
    [SerializeField]
    private bool IsNewLap = false;

    private bool Boostbool = false;

    const float MaxBoostTime = 1f;
    float CurrentBoostTime = 1f;

    void Awake()
    {
        BoostParticles = BoostParticleParent.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem p in BoostParticles)
        {
            p.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = KartSphere.transform.position - new Vector3(0f, 0.8f, 0);
        //make kart follow sphere

        //Go Forward
        if (Input.GetAxis("Vertical") != 0)
        {
            if (!Stunned)
            {
                int forward = Input.GetAxis("Vertical") > 0 ? 1 : -1;
                if (forward == 1)
                {
                    speed = TopSpeed;
                    if (Boostbool)
                    {
                        speed = TopSpeed + TopSpeed * 1.5f;
                    }
                }
                else
                {
                    speed = TopSpeed * -0.25f;
                }
            }
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
                control = 0.5f + (Input.GetAxis("Horizontal")* driftingability);
            } else
            {
                driftdebug.text = "Direction: Left";
                control = 0.5f + (Input.GetAxis("Horizontal") * -driftingability);
            }

            driftPower = driftPower + Time.deltaTime;


            driftdebug.text = driftdebug.text + "\n Control: " + control.ToString();
            driftdebug.text = driftdebug.text + "\n DriftPower" + driftPower.ToString();
            debug.text = "Drifting: True";

            Steer(driftDirection, control);
        } else {
            debug.text = "Drifting: False";
            CalculateBoost(driftPower);
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
            if (!currentlyboosting)
            {
                foreach (ParticleSystem p in BoostParticles)
                {
                    p.Play();
                }
                currentlyboosting = true;
            }

            CurrentBoostTime = CurrentBoostTime - Time.deltaTime;
            if (CurrentBoostTime < 0)
            {
                foreach (ParticleSystem p in BoostParticles)
                {
                    p.Stop();
                }
                currentlyboosting = false;
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

    private void CalculateBoost(float boostPower)
    {
        if (boostPower > 7)
        {
            Boostbool = true;
            CurrentBoostTime = 1;
            driftPower = 0;
            return;
        }

        else if (boostPower > 5)
        {
            Boostbool = true;
            CurrentBoostTime = 0.5f;
            driftPower = 0;
            return;
        }

        else if (boostPower > 3)
        {
            Boostbool = true;
            CurrentBoostTime = 0.2f;
            driftPower = 0;
            return;
        }

        driftPower = 0;
    }




    /*
    /   Figuring Out Who's Where
    */

    public int GetTargetCheckPoint()
    {
        return TargetCheckPoint;
    }

    public void SetTargetCheckPoint(int IncVal)
    {
        TargetCheckPoint = IncVal;
    }

    public int GetCurrentCheckpoint()
    {
        return CurrentCheckPoint;
    }

    public void SetCurrentCheckpoint(int IncVal)
    {
        CurrentCheckPoint = IncVal;
    }

    public float GetCheckPointValue()
    {
        return CheckPointValue;
    }

    public void SetCheckPointValue(float IncVal)
    {
        CheckPointValue = CheckPointValue + IncVal;
    }

    public string GetName()
    {
        return Name;
    }

    public bool GetIsNewLap()
    {
        return IsNewLap;
    }

    public void SetIsNewLap(bool YesNo)
    {
        IsNewLap = YesNo;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void BecomeStunned()
    {
        if (!Stunned)
        {
            StartCoroutine("StunnedCoroutine");
        }
        Stunned = true;
    }

    IEnumerator StunnedCoroutine()
    {
        yield return new WaitForSeconds(2.5f);
        Stunned = false;
    }
}
