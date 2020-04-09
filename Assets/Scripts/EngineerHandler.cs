using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class EngineerHandler : MonoBehaviour
{
    #region Singleton
    internal static EngineerHandler instance = null;
    void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }
    }
    #endregion

    #region Character Attributesd
    //private CharacterController controller;
    private BoxCollider bcol;
    internal Animator anim;
    public bool canMove = true;
    internal bool engineerMove = true; //variable to be used by the GameManager.cs in order to control whether the Engineer Robot can move
    //private float speed = 4.0f;
    //private float rotateSpeed = 0.2f;
    #endregion


    [Header("Sphere Game Object")]
    [SerializeField] internal GameObject sphere;
    [Tooltip("Sphere Tag is the string to be parsed into the script. Need to be the same as the tag of the Sphere GameObject")]
    public string sphereTag = "PlayerSphere";

    [Space]
    [Tooltip("This inspector variable is to check if the box to be carried is being parsed to the Engineer Script when triggered")]
    public GameObject boxToCarry; //This inspector variable is to check if the box to be carried is being parsed to the Engineer Script when triggered

    void Start()
    {
        bcol = GetComponent<BoxCollider>();
        bcol.enabled = false;
        //controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        sphere = GameObject.FindGameObjectWithTag(sphereTag);
    }

    void Update()
    {
        if (engineerMove)
        {
            //if (controller.isGrounded)
            //{
                if (Input.GetButtonDown("Action3")) { TakeObject(); }
                if (!anim.GetBool("CarryObject"))
                {
                    if (Input.GetButtonDown("Action4")) { Action(); }
                    if (Input.GetButtonDown("Action1"))
                    {
                        Jump();
                        FindObjectOfType<AIUI>().ShowText($"You can not jump, Sir. You are very heavy. Try to ask the {GameManager.sphereName} to jump for you.");
                    }
                    if (Input.GetButtonDown("Action2")) { Punch(); }
                }
            //}
        }
    }

    void LateUpdate()
    {
        if (engineerMove)
        {
            //if (controller.isGrounded)
            //{
                Move();
            //}
        }
        else
        {
            anim.SetFloat("Horizontal", 0);
            anim.SetFloat("Vertical", 0);
        }
    }

    public void TakeObject()
    {
        if (anim.GetBool("CarryObject"))
        {
            StartCoroutine(CantMove(3.0f));
            anim.SetBool("CarryObject", false);
            if (boxToCarry)
            {
                boxToCarry.GetComponent<PuzzleCubes>().OnRelease();
                bcol.enabled = false;
            }
        }
        else
        {
            StartCoroutine(CantMove(3.0f));
            anim.SetTrigger("TakeObject");
            if (boxToCarry)
            {
                anim.SetBool("CarryObject", true);
                bcol.enabled = true;
            }
        }
    }

    public void Action()
    {
        anim.SetTrigger("Action");
    }

    public void Jump()
    {
        anim.SetTrigger("Jump");
    }

    public void Punch()
    {
        anim.SetTrigger("Punch");
    }

    public void Move()
    {
        if (canMove)
        {
            anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
            anim.SetFloat("Vertical", Input.GetAxis("Vertical"));
        }
        else
        {
            anim.SetFloat("Vertical", 0);
            anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        }
    }

    public void PickItem()
    {
        if (boxToCarry) boxToCarry.GetComponent<PuzzleCubes>().OnCarry();
    }

    IEnumerator CantMove(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
        StopCoroutine("CantMove");
    }
}
