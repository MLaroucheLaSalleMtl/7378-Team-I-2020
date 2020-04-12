﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//by Philipe Gouveia

public class MechHandler : MonoBehaviour
{
    #region Singleton
    public static MechHandler instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject); }
    }
    #endregion

    #region CharacterHandler
    private Rigidbody rb;
    internal bool switchOn; //******** change to private after test
    private Vector3 pos;
    private Quaternion rot;
    public float speed;
    public float rateOfTurn;
    public float canonSpeed;
    [SerializeField] private GameObject forceField;
    [SerializeField] private Transform body;
    #endregion

    #region Controlllers
    private Animator anim;
    private AudioSource audio;
    #endregion

    #region Canon
    [Space]
    [Header("Canon")]
    //canon shoot
    [SerializeField] private LineRenderer[] laserBeans;
    private int lastIndex = 0;
    [SerializeField] private GameObject laserAim;
    [SerializeField] private GameObject particleExplosion;
    [SerializeField] private GameObject[] enemy;
    private RaycastHit hit;
    private Ray ray;

    //rotation not working as desired
    private Vector3 canonRotInput;
    private float rotateCanon;
    public float angleLimit = 60f;
    private Vector3 smoothCanonRot;
    [SerializeField] private Transform canons;
    #endregion

    #region Bars
    [Space]
    [Header("Bars")]
    [SerializeField] private Text integrityTxt;
    [Range(0, 3)] public float shoots = 3.0f;
    private float defaultShoots = 3.0f;
    [SerializeField] private Text shieldTxt;
    [Range(0, 4)] public float shieldBar = 3.1f;
    private float defaultShield = 3.1f;
    [SerializeField] private Text shootsTxt;
    [Range(0, 9)] public int healthBar = 9;
    private int defaultHealth = 9;

    private bool reset = true;
    #endregion


    #region AudioClips
    [Space]
    [Header("AudioClips")]
    [SerializeField] AudioClip step;
    [SerializeField] AudioClip[] shoot;
    [SerializeField] AudioClip explosion;
    #endregion

    void Start()
    {
        forceField.SetActive(false);
        switchOn = false;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        reset = true;
        canons.rotation = Quaternion.Euler(0, 0, -90);

        Invoke("SwitchON", 5f);
    }

    void Update()
    {
        if (switchOn)
        {
            anim.SetBool("SwitchOn", true);
        }


        if (anim.GetBool("SwitchOn"))
        {
            if (Input.GetButton("Action1")) ForceField();
            if (Input.GetButtonUp("Action1")) forceField.SetActive(false);
            if (!forceField.activeSelf)
            {
                if (Input.GetButtonDown("Action4"))
                {
                    Shoot();
                }
            }
            else
            {
                if (Input.GetButtonDown("Action4"))
                {
                    FindObjectOfType<AIUI>().ShowText("You can not shoot while the shield is active.");
                }
            }

            Move();
            //RotateCanon(); NOT WORKING AS DESIRED
        }

        if (Input.GetKeyDown(KeyCode.Alpha0)) //**********tester
        {
            GetHit();
        }
    }

    public void SwitchOn()
    {
        switchOn = true;
    }

    public void Move()
    {
        if (Input.GetButton("Vertical") && Input.GetAxis("Vertical") >= 0)
        {
            anim.SetBool("Walk", true);
            rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
            pos = transform.position + (transform.forward * (Input.GetAxis("Vertical") * speed * Time.deltaTime));
            rb.MovePosition(pos);

            rot = transform.rotation * Quaternion.Euler(Vector3.up * (rateOfTurn * Input.GetAxis("Horizontal") * Time.deltaTime));
            rb.MoveRotation(rot);
        }

        else
        {
            anim.SetBool("Walk", false);
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        BarChecker();
    }

    public void RotateCanon()
    {
        if (canons)
        {
            //canonRotInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(canonRotInput);
            //canons.rotation = Quaternion.Lerp(canons.rotation, Quaternion.Euler(0, canonRotInput.x * canonSpeed, -90), 1);

        //OR

            //canonRotInput = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            //rotateCanon = canons.rotation.x + (canonSpeed * (0.5f - canonRotInput.x));
            //smoothCanonRot = Vector3.Lerp(smoothCanonRot, rotateCanon, Time.deltaTime * canonSpeed);
            //canons.rotation = Quaternion.Euler(0, -smoothCanonRot + body.rotation.x, -90);

        }
    } //not working properly

    public void Step()
    {
        audio.PlayOneShot(step, 0.5f);
    }

    public void ForceField()
    {
        if (shieldBar >= 1)
        {
            forceField.SetActive(true);
            if (shieldBar <= 1)
            {
                shieldTxt.color = Color.red;
            }
            else if (shieldBar > 1)
            {
                shieldTxt.color = Color.blue;
            }
        }
        else if (shieldBar < 1)
        {
            forceField.SetActive(false);
            FindObjectOfType<AIUI>().ShowText("Wait for the shield to reload.");
        }
    }

    public void Shoot()
    {
        if (shoots >= 1)
        {
            anim.SetTrigger("Shoot");
            audio.PlayOneShot(shoot[Random.Range(0, 2)], 0.5f);
            ShootCanon();
            shoots--;
            shootsTxt.text = "";
            for (int i = 1; i <= shoots; i++)
            {
                shootsTxt.text += '|';
            }
            if (shoots <= 1)
            {
                shootsTxt.color = Color.red;
            }
            else
            {
                shootsTxt.color = Color.blue;
            }
        }
        else if (shoots < 1)
        {
            FindObjectOfType<AIUI>().ShowText("Wait for the canon to reload.");
        }
    }

    private void TargetHit()
    {
        Debug.DrawRay(laserAim.transform.position, laserAim.transform.TransformDirection(Vector3.forward) * 100f, Color.green, 1.5f);

        if (Physics.Raycast(laserAim.transform.position, laserAim.transform.TransformDirection(Vector3.forward), out hit, 100f))
        {
            Instantiate(particleExplosion, hit.point, Quaternion.identity);

            if (hit.transform.tag == "Enemy")
            {
                Debug.Log("Hit"); //phil: Code to reduce enemy healthBar - Waiting on Sidakpreet to finish enemy
            }
        }
    }

    public void BarChecker()
    {
        if (shoots <= 0 && reset)
        {
            StartCoroutine(ReloadBar(shootsTxt, defaultShoots));
        }

        if (shieldBar <= 0.2 && reset)
        {
            StartCoroutine(ReloadBar(shieldTxt, defaultShield));
        }
    }

    public void GetHit()
    {
        audio.PlayOneShot(explosion, 0.3f);

        if (forceField.activeSelf)
        {
            shieldBar--;
            shieldTxt.text = "";
            for (int i = 1; i <= shieldBar; i++)
            {
                shieldTxt.text += '|';
            }
            if (shieldBar <= 1)
            {
                shieldTxt.color = Color.red;
            }
            else
            {
                shieldTxt.color = Color.blue;
            }
        }
        else
        {
            healthBar--;
            integrityTxt.text = "";

            for (int i = 0; i <= healthBar; i++)
            {
                integrityTxt.text += '|';
            }
            if (healthBar <= 3)
            {
                integrityTxt.color = Color.red;
            }
        }

        if (healthBar < 0)
        {
            anim.SetBool("Dead", true);
            Invoke("Die", 3f);
        }
    }

    IEnumerator ReloadBar(Text txt, float size)
    {
        reset = false;

        txt.text = "";
        int counter = 1;

        while (counter <= size)
        {
            yield return new WaitForSeconds(1.0f);
            txt.text += '|';
            counter++;
        }

        txt.color = Color.blue;

        if (size == 3.1f) shieldBar = size;
        if (size == 3.0f) shoots = size;

        reset = true;
        StopCoroutine(ReloadBar(txt, size));
        yield return null;
    }

    private void Die()
    {
        PortalToMaze.skipPortal = true;
        FindObjectOfType<GameManager>().LoadNewLevel("FinalBoss");
        FindObjectOfType<GameManager>().ActivateNewScene();
    }

    public void SwitchON()
    {
        switchOn = true;
        CameraRigHandler.stageIndex = 1;
    }

    /*-------Methods from Unity Asset------*/
    //some changes were necessary to apply the script for the array of laserbeans
    private void ShootCanon()
    {
        lastIndex = 1 - lastIndex;
        Color c = laserBeans[lastIndex].material.GetColor("_TintColor");
        c.a = 1f;
        laserBeans[lastIndex].material.SetColor("_TintColor", c);

        StartCoroutine(FadeLaserBeam(lastIndex));

        TargetHit();
    }

    IEnumerator FadeLaserBeam(int index)
    {
        Color c = laserBeans[index].material.GetColor("_TintColor");
        while (c.a > 0)
        {
            c.a -= 0.1f;
            laserBeans[index].material.SetColor("_TintColor", c);
            yield return null;
        }
    }
}
