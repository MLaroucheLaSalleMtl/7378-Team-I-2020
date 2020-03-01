using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPuzzle_2 : MonoBehaviour
{
    #region Object attributes
    private EngineerHandler engineer;
    private Rigidbody rb;
    private BoxCollider bc;
    private Material mat;
    #endregion

    [Space]
    [SerializeField] Transform grabPos;
    [Tooltip("Child GameObject of Engineer where the box will be attached")]
    public string engineerGrabPos = "GrabPos";
    [Space]
    [Tooltip("Tag on the Engineer Player")]
    public string engineerTag;
    [Space]
    public string switchTag = "switchForBox";

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        mat = GetComponent<Renderer>().material;
        engineer = GameObject.FindObjectOfType<EngineerHandler>();
        engineerTag = GameManager.engineerTag;
        grabPos = GameObject.FindWithTag(engineerGrabPos).transform; //Assign Transform of child gameobject located on the hand of the Engineer Robot
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == engineerTag)
        {
            engineer.boxToCarry = this.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == engineerTag)
        {
            engineer.boxToCarry = null;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == switchTag)
        {
            mat.SetColor("_EmissionColor", Color.green);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == switchTag)
        {
            mat.SetColor("_EmissionColor", Color.red);
        }
    }

    public void OnCarry()
    {
        rb.useGravity = false;
        rb.isKinematic = true;
        transform.position = grabPos.transform.position;
        transform.SetParent(grabPos.transform);
    }

    public void OnRelease()
    {
        rb.useGravity = true;
        rb.isKinematic = false;
        transform.SetParent(null);
        engineer.boxToCarry = null;
    }
}
