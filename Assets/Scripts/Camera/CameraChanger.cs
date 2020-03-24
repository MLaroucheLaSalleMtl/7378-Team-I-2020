using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == GameManager.sphereTag) //did not put the engineer as the sphere will always be alongside the engineer
        {
            float direction = Vector3.Angle(transform.forward, (other.gameObject.transform.position - this.transform.position));

            if (direction > 90f )
            {
                CameraRigHandler.stageIndex--;
            }
            else
            {
                CameraRigHandler.stageIndex++;
            }
        }
    }
}
