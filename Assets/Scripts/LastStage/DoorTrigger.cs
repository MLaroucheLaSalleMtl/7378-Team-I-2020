using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private Animator anim;
    public bool stageEntrance;
    public string aiText;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!stageEntrance)
        {
            if (other.gameObject.tag == GameManager.engineerTag)
            { anim.SetBool("isOpen", true); }
        }
        if (stageEntrance)
        {
            FindObjectOfType<AIUI>().ShowText(aiText);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == GameManager.engineerTag)
        { anim.SetBool("isOpen", false); }
    }

}
