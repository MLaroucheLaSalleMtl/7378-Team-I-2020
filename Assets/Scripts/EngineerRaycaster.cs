using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerRaycaster : MonoBehaviour
{
    private RaycastHit hit;
    private EngineerHandler engineer;
    public bool groundCaster = false;
    public bool frontCaster = false;

    private void Start()
    {
        engineer = FindObjectOfType<EngineerHandler>();
    }

    private void Update()
    {
        if (groundCaster)
        {
            if (Physics.Raycast(transform.position, -transform.TransformDirection(Vector3.up), out hit, 0.1f))
            {
                Debug.DrawRay(transform.position, -transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
                engineer.canMove = true;
            }
            else
            {
                Debug.DrawRay(transform.position, -transform.TransformDirection(Vector3.up) * 1f, Color.white);
                if (Physics.Raycast(transform.position, -transform.TransformDirection(Vector3.up), out hit))
                {
                    if (hit.transform.tag == "gap") engineer.canMove = false;
                }
            }
        }
        //if (frontCaster && engineer.anim.GetBool("CarryObject"))
        //{
        //    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.05f))
        //    {
        //        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        //        if (hit.transform.tag != "boxSwitch") engineer.TakeObject();
        //    }
        //    else
        //    {
        //        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 0.05f, Color.white);
        //        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * 1.2f, out hit))
        //        //{
        //        //    engineer.canMove = true;
        //        //}
        //    }
        //}
    }

}
