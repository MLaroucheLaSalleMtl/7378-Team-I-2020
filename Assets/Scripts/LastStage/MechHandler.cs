using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool switchOn; //change to private after test
    private float rotMax = 90f;
    [SerializeField] private GameObject forceField;
    [SerializeField] private Transform canons;
    [SerializeField] private Transform targetIK;
    #endregion

    #region Controlllers
    private Animator anim;
    private AudioSource audio;
    [SerializeField] private Camera cam;
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
    }

    void Update()
    {
        if (switchOn)
        {
            anim.SetBool("SwitchOn", true);
        }


        if (anim.GetBool("SwitchOn"))
        {
            if (Input.GetButton("Action3")) ForceField(); 
            if (Input.GetButtonUp("Action3")) forceField.SetActive(false);
            if (Input.GetButtonDown("Action4"))
            {
                anim.SetTrigger("Shoot");
                audio.PlayOneShot(shoot[Random.Range(0, 2)], 0.5f);
            }

            Move();
        }
    }

    //private void OnAnimatorIK(int layerIndex) //function from the Unity FrameWork that recognizes the IK target
    //{
    //    anim.SetIKPosition(AvatarIKGoal.RightHand, targetIK.position);
    //    anim.SetIKPositionWeight(AvatarIKGoal.RightHand, weightForPosition);

    //    anim.SetIKRotation(AvatarIKGoal.RightHand, rightArmTarget.rotation);
    //    anim.SetIKRotationWeight(AvatarIKGoal.RightHand, weightForRotation);

    //    anim.SetLookAtPosition(rightArmTarget.position); //anim rig to use at
    //    anim.SetLookAtWeight(weightForLook, bodyWeight, headWeight, eyesWeight, clampWeight);
    //}

    public void SwitchOn()
    {
        switchOn = true;
    }

    public void Move()
    {
        //float rotX = Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;

        //Vector3 cursorPos = Input.mousePosition;
        //Ray ray = cam.ScreenPointToRay(cursorPos);
        //targetIK.position = ray.GetPoint(10);

        //if (Input.GetButton("Action1"))
        //{
        //    float rotX = Input.GetAxis("Horizontal") * Time.deltaTime * rotMax;
        //    canons.Rotate(0, rotX, 0);
        //    Debug.Log(rotX);
        //}
    }

    public void Step()
    {
        audio.PlayOneShot(step, 0.5f);
    }

    public void ForceField()
    {
        forceField.SetActive(true);
    }
}
