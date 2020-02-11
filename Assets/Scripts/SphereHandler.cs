using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereHandler : MonoBehaviour
{
    #region Singleton
    internal static SphereHandler instance = null;
    void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }
    }
    #endregion

    #region Character Attributes
    internal Rigidbody rb;
    internal Animator anim;
    [Header("Engineer Game Object")]
    [SerializeField] internal GameObject engineer;
    [Space]
    [Header("Colliders of Sphere Body")]
    [Tooltip("Collider located on the body of the sphere so the game object can handle it during movements ")]
    [SerializeField] internal SphereCollider scol;
    [SerializeField] internal BoxCollider bcol;

    private float speed = 2.0f;
    internal bool sphereMove = true; //variable to control whether the Engineer or the Sphere sphereMove
    #endregion

    #region Follow Engineer Attributes
    private float followMaxDistance = 3.0f;
    private float followMinDistance = 0.5f;
    private float followSpeed = 3.0f;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        bcol.enabled = false;
    }

    void Update()
    {
        if (sphereMove)
        {
            if (Input.GetKeyUp(KeyCode.Q)) { Open(); }
            if (Input.GetKeyUp(KeyCode.Space)) { Jump(); }
            if (Input.GetKey(KeyCode.LeftAlt)) { Attack(true); }
            if (Input.GetKeyUp(KeyCode.LeftAlt)) { Attack(false); }
            if (Input.GetKey(KeyCode.E)) { Action(); }
        }
        else
        {
            FollowMove();
        }
    }

    void FixedUpdate()
    {
        if (sphereMove)
        {
            if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0))
            {
                anim.SetBool("Movement", true);
                Move();
            }

            if ((Input.GetAxis("Horizontal") == 0) && (Input.GetAxis("Vertical") == 0))
            {
                rb.velocity = Vector3.zero;
                anim.SetBool("Movement", false);
            }
        }
    }

    public void Attack(bool cond)
    {
        rb.transform.rotation = Quaternion.identity;
        anim.SetBool("Attack", cond);
    }

    public void Action()
    {
        if (anim.GetBool("Open"))
        {
            rb.transform.rotation = Quaternion.identity;
            anim.SetTrigger("Action");
        }
    }

    public void Open()
    {
        rb.transform.rotation = Quaternion.identity;
        anim.SetBool("Open", !anim.GetBool("Open"));
        if (anim.GetBool("Open"))
        {
            bcol.enabled = true;
            rb.freezeRotation = true;
        }
        else
        {
            bcol.enabled = false;
            rb.freezeRotation = false;
        }
    }

    public void Move()
    {
        if (anim.GetBool("Open"))
        {
            rb.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
        else
        {
            rb.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            rb.AddForce(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed, ForceMode.Acceleration);
        }
    }

    private void FollowMove()
    {
        if (Vector3.Distance(transform.position, engineer.transform.position) >= followMaxDistance)
        {
            rb.AddForce((engineer.transform.position - transform.position) * followSpeed * Time.deltaTime, ForceMode.VelocityChange);
            transform.LookAt(engineer.transform);
        }
        else if (Vector3.Distance(transform.position, engineer.transform.position) <= followMinDistance)
        {
            rb.velocity = Vector3.zero;
        }
    }

    public void Jump()
    {
        rb.transform.rotation = Quaternion.Euler(Vector3.zero);
        anim.SetTrigger("Jump");
        StartCoroutine(Jump(3.0f));
    }

    IEnumerator Jump(float time)
    {
        rb.freezeRotation = true;
        rb.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        yield return new WaitForSeconds(time * 0.2f);
        scol.radius = 0.004f;
        yield return new WaitForSeconds(time * 0.8f);
        if (!anim.GetBool("Open")) rb.freezeRotation = false;
        scol.radius = 0.002f;
        StopCoroutine("Jump");
    }
}
