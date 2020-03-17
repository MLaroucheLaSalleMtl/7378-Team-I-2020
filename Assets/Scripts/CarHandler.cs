using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarHandler : MonoBehaviour
{
    #region Singleton Pattern
    internal static CarHandler instance = null;
    void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }
    }
    #endregion

    #region Class Attributes
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private WheelCollider wheelFLCol, wheelFRCol, wheelRLCol, wheelRRCol;
    [SerializeField] private Transform wheelFLTrans, wheelFRTrans, wheelRLTrans, wheelRRTrans;
    #endregion

    #region Movement Variables
    [SerializeField] [Range(1f, 45f)] private float steeringAngle;
    [SerializeField] [Range(1f, 45f)] private float maxSteeringAngle = 30f;
    [SerializeField] [Range(1f, 1000f)] private float maxTorque = 500f;
    [SerializeField] [Range(1f, 100000f)] private float brakeTorque = 9000f;
    public float maxSpeed = 20f;
    internal bool carMove; //variable to control whether the Engineer or the Sphere sphereMove
    public bool carParked; //car State
    private int speedForText;
    [SerializeField] private Text speedTxt;
    private int angleForText;
    [SerializeField] private Text angleTxt;
    #endregion

    [SerializeField] private GameObject engineer;
    [SerializeField] private GameObject sphere;

    void Start()
    {
        carMove = false;
        if (carParked) anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        engineer = GameObject.FindGameObjectWithTag(GameManager.engineerTag);
        sphere = GameObject.FindGameObjectWithTag(GameManager.sphereTag);
        if (carParked) rb.isKinematic = true;
    }

    private void Update()
    {
        if (carParked)
        {
            anim.enabled = true;

            if (anim.GetBool("isOpen"))
            {
                rb.isKinematic = false;
            }
        }

        if (!carParked)
        {
            rb.isKinematic = false;
        }
    }

    private void FixedUpdate()
    {
        if (!carParked)
        {
            Move();

            Brake();

            if (speedTxt)
            {
                speedForText = (int)(rb.velocity.magnitude * 3.6f);
                speedTxt.text = speedForText.ToString() + " Kph";
            }
            if (angleTxt)
            {
                angleForText = (int)(rb.angularVelocity.z);
                angleTxt.text = angleForText.ToString();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (carParked)
        {
            if (other.gameObject.tag == GameManager.engineerTag)
            {
                if (Input.GetButtonDown("Action4"))
                {
                    carMove = true;
                    FindObjectOfType<EngineerHandler>().engineerMove = false;
                    FindObjectOfType<SphereHandler>().sphereMove = false;
                    engineer.transform.parent = gameObject.transform;
                    sphere.transform.parent = gameObject.transform;
                    engineer.SetActive(false);
                    sphere.SetActive(false);
                    rb.isKinematic = false;
                    rb.constraints = RigidbodyConstraints.FreezeRotation;
                    anim.SetBool("Close", true);
                    anim.SetTrigger("Movefwd");
                }
            }
            else if (other.gameObject.tag == GameManager.sphereTag)
            {
                FindObjectOfType<AIUI>().ShowText("Only the engineer can drive the car");
            }
        }
    }

    private void Move()
    {
        steeringAngle = Input.GetAxis("Horizontal") * maxSteeringAngle;
        wheelFLCol.steerAngle = steeringAngle;
        wheelFRCol.steerAngle = steeringAngle;

        if (rb.velocity.magnitude < maxSpeed)
        {
            wheelFLCol.motorTorque = Input.GetAxis("Vertical") * maxTorque;
            wheelFRCol.motorTorque = Input.GetAxis("Vertical") * maxTorque;
            wheelRLCol.motorTorque = Input.GetAxis("Vertical") * maxTorque;
            wheelRRCol.motorTorque = Input.GetAxis("Vertical") * maxTorque;
        }

        WheelHandler(wheelFLCol, wheelFLTrans);
        WheelHandler(wheelFRCol, wheelFRTrans);
        WheelHandler(wheelRLCol, wheelRLTrans);
        WheelHandler(wheelRRCol, wheelRRTrans);
    }

    private void WheelHandler(WheelCollider wheel, Transform trans)
    {
        Vector3 posit = trans.position;
        Quaternion quatern = trans.rotation;

        wheel.GetWorldPose(out posit, out quatern);

        trans.position = posit;
        trans.rotation = quatern;
    }

    private void Brake()
    {
        if (Input.GetButton("Action1"))
        {
            wheelFLCol.brakeTorque = brakeTorque;
            wheelFRCol.brakeTorque = brakeTorque;
            wheelRLCol.brakeTorque = brakeTorque;
            wheelRRCol.brakeTorque = brakeTorque;
        }
        else
        {
            wheelFLCol.brakeTorque = 0;
            wheelFRCol.brakeTorque = 0;
            wheelRLCol.brakeTorque = 0;
            wheelRRCol.brakeTorque = 0;
        }
    }
}
