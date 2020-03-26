using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Sohyun Yi and Philipe

public class SwitchFloor : MonoBehaviour
{
    [SerializeField] private Material mat;
    public string boxTag = "boxSwitch";
    [SerializeField] private GameObject activateElement;
    [SerializeField] private ItemHighlight highlight;

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
        if (highlight) highlight.blink = false;
        mat.SetColor("_EmissionColor", Color.green);
        activateElement.GetComponent<Animator>().SetBool("isOpen", true);
    }

    public void OnClose()
    {
        if (highlight) highlight.blink = false;
        mat.SetColor("_EmissionColor", Color.red);
        activateElement.GetComponent<Animator>().SetBool("isOpen", false);
    }
}
