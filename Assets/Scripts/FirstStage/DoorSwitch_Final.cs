using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch_Final : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private string playerTag = GameManager.engineerTag;
    [SerializeField] private Animator animLeft;
    [SerializeField] private Animator animRight;

    void Start()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
            if (Input.GetButton("Action4"))
            {
                OnClick();
            }
        }
    }

    public void OnClick()
    {
        mat.SetColor("_EmissionColor", Color.green);
        animLeft.SetBool("IsOpen", true);
        animRight.SetBool("IsOpen", true);
        FindObjectOfType<AIUI>().ShowText("You escaped from this place.");

    }
}
