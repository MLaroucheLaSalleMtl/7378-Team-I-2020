using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWall : MonoBehaviour
{

    [Header("Result of this switch")]
    [SerializeField] private GameObject resultObj;
    [Header("Player who will act on this switch:")]
    public bool sphereAct;
    public bool engineerAct;
    [Space]
    [Header("Output AI instruction?")]
    public bool outputInstruction;
    public string aiInstruction;

    private string playerTag;
    private Material mat;
    private bool activated;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        if (sphereAct) { playerTag = GameManager.sphereTag; }
        else { playerTag = GameManager.engineerTag; }
        outputInstruction = false;
        activated = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
            if (Input.GetButton("Action4"))
            {
                if (!activated)
                {
                    OnClick();
                    activated = true;
                }
                else
                {
                    SwitchOff();
                    activated = false;
                }
            }
        }
    }

    public void OnClick()
    {
        mat.SetColor("_EmissionColor", Color.green);
        resultObj.GetComponent<Animator>().SetBool("isOpen", true);
        if (outputInstruction) { FindObjectOfType<AIUI>().ShowText(aiInstruction); }
    }

    public void SwitchOff()
    {
        {
            mat.SetColor("_EmissionColor", Color.red);
            resultObj.GetComponent<Animator>().SetBool("isOpen", false);
        }
    }
}
