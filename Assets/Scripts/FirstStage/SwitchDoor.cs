using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDoor : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private string boxTag = "boxSwitch";
    [SerializeField] private GameObject activateElement;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == boxTag)
        {
            OnOpen();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == boxTag)
        {
            OnClose();
        }
    }

    public void OnOpen()
    {
        mat.SetColor("_EmissionColor", Color.green);
        activateElement.GetComponent<Animator>().SetBool("openDoor", true);
    }

    public void OnClose()
    {
        mat.SetColor("_EmissionColor", Color.red);
        activateElement.GetComponent<Animator>().SetBool("openDoor", false);
    }
}
