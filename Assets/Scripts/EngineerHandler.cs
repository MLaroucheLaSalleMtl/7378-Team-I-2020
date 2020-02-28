using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private BoxCollider bcol;
    private CharacterController controller;
    internal Animator anim;
    private bool canMove = true;
    private float speed = 4.0f;
    private float rotateSpeed = 0.5f;
    internal bool engineerMove = true; //variable to be used by the GameManager.cs in order to control whether the Engineer Robot can move
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
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        sphere = GameObject.FindGameObjectWithTag(sphereTag);
    }

    void Update()
    {
        if (engineerMove)
        {
            if (controller.isGrounded)
            {
                if (Input.GetButtonUp("Action3")) { TakeObject(); }
                if (!anim.GetBool("CarryObject"))
                {
                    if (Input.GetButtonUp("Action4")) { Action(); }
                    if (Input.GetButtonUp("Action1")) 
                    { 
                        Jump();
                        FindObjectOfType<AIUI>().ShowText("You can not jump, Sir. You are very heavy. Try to ask the ___SPHERE___ to jump for you.");
                    }
                    if (Input.GetButtonUp("Action2")) { Punch(); }
                }
            }
        }
    }

    void LateUpdate()
    {
        if (engineerMove)
        {
            if (controller.isGrounded && canMove)
            {
                if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") > 0))
                {
                    anim.SetBool("Movement", true);
                    Move();
                }

                else if ((Input.GetAxis("Horizontal") == 0) && (Input.GetAxis("Vertical") == 0))
                {
                    anim.SetBool("Movement", false);
                    controller.Move(Vector3.zero);
                }
            }
        }
        else
        {
            anim.SetBool("Movement", false); //To be removed
        }
    }

    public void TakeObject()
    {
        if (anim.GetBool("CarryObject"))
        {
            StartCoroutine(CantMove(3.0f));
            anim.SetBool("CarryObject", false);
            if (boxToCarry) boxToCarry.GetComponent<PuzzleCubes>().OnRelease();
        }
        else
        {
            StartCoroutine(CantMove(3.0f));
            anim.SetTrigger("TakeObject");
            if (boxToCarry)
            {
                anim.SetBool("CarryObject", true);
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
        if (controller.isGrounded)
        {
            anim.SetFloat("Horizontal", Input.GetAxis("Horizontal") * speed);
            anim.SetFloat("Vertical", Input.GetAxis("Vertical") * rotateSpeed);
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
