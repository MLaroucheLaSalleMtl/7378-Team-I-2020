using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterStageTester : MonoBehaviour
{
    private bool doOnce = true;
    public string stageName;

    private void Start()
    {
        doOnce = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == GameManager.carTag)
        {
            if (Input.GetButton("Action4"))
            {
                Invoke("GoInsideShip", 2f);
            }
        }
    }

    void GoInsideShip()
    {
        if (doOnce)
        {
            doOnce = false;
            //s.BtnLoadScene(stageName);
            GameManager gm = GameManager.instance;
            doOnce = false;
            gm.LoadNewLevel(stageName);
            gm.ActivateNewScene();
        }
    }
}
