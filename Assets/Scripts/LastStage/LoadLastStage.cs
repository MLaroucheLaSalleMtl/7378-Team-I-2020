using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLastStage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameManager.engineerTag)
        {
            FindObjectOfType<GameManager>().LoadNewLevel(5);
            FindObjectOfType<AIUI>().ShowText("After the passing through the teleport portal you will not be allowed to go back");
        }
    }
}
