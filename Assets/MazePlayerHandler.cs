using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class MazePlayerHandler : MonoBehaviour
{
    #region Singleton
    public static MazePlayerHandler instance;
    public void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }
    }
     #endregion

    private Rigidbody rb;
    private float horizontal;
    private float vertical;
    private Vector3 movement;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        movement = new Vector3(horizontal, 0, vertical);
        rb.AddForce(movement * speed * Time.deltaTime, ForceMode.VelocityChange);
    }
}
