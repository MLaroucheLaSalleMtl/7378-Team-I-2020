using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoles : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == GameManager.sphereTag)
        {
            FindObjectOfType<MazePlayerHandler>().GetComponent<Rigidbody>().AddForce(-transform.forward);
        }
    }
}
