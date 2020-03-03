using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionTrigger : MonoBehaviour
{
    public string instruction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameManager.engineerTag || other.gameObject.tag == GameManager.sphereTag)
        {
            FindObjectOfType<AIUI>().ShowText(instruction);
        }
    }
}
