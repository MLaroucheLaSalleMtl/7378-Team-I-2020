using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalAfterTetris : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.tag == GameManager.engineerTag)
        {
            Invoke("NewLevel", 1.5f);
        }
    }

    private void NewLevel()
    {
        FindObjectOfType<GameManager>().ActivateNewScene();
    }
}
