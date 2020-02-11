using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCubes : MonoBehaviour
{
    [SerializeField] GameObject grabPos;
    private EngineerHandler player = EngineerHandler.instance;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    internal void OnCarry()
    {
        rb.useGravity = false;
        transform.position = grabPos.transform.position;
        transform.SetParent(grabPos.transform);
    }

    internal void OnRelease()
    {
        rb.useGravity = true;
        transform.SetParent(null);
        player.objectToCarry = null;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.objectToCarry = this.gameObject;
        }
    }
}
