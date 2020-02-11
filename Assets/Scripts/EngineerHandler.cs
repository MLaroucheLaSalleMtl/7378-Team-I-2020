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
    private Rigidbody rb;
    internal Animator anim;

    [Header("Sphere Game Object")]
    [SerializeField] internal GameObject sphere;

    private float speed = 1.5f;
    internal bool engineerMove = true; //variable to be used by the GameManager.cs in order to control whether the Engineer Robot can move
    #endregion

    public GameObject objectToCarry;

    void Start()
    {
        bcol = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (engineerMove)
        {
            if (Input.GetKeyUp(KeyCode.Q)) { TakeObject(); }
            if (Input.GetKeyUp(KeyCode.E)) { Action(); }
            if (Input.GetKeyUp(KeyCode.Space)) { Jump(); }
            if (Input.GetKeyUp(KeyCode.LeftAlt)) { Punch(); }
        }
    }

    void LateUpdate()
    {
        if (engineerMove)
        {
            anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
            anim.SetFloat("Vertical", Input.GetAxis("Vertical"));
            anim.SetBool("Movement", true); //To be removed
            Move();
        }
        else
        {
            anim.SetBool("Movement", false); //To be removed
        }
    }

    public void TakeObject()
    {
        if (!anim.GetBool("CarryObject"))
        {
            anim.SetTrigger("TakeObject");
            if (objectToCarry) 
            {
                objectToCarry.GetComponent<PuzzleCubes>().OnCarry();
                anim.SetBool("CarryObject", true);
            }
        }
        else
        {
            anim.SetBool("CarryObject", false);
            if (objectToCarry) objectToCarry.GetComponent<PuzzleCubes>().OnRelease();
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
        // anim.SetFloat("Horizontal", Input.GetAxis("Horizontal") * speed * Time.deltaTime);
        // anim.SetFloat("Vertical", Input.GetAxis("Vertical") * speed * Time.deltaTime);

        rb.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") * speed);
    }
}
