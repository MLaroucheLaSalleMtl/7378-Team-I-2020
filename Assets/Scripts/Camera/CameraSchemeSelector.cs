     using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

//Trigger to put through the stage so the CameraRigHandler can change the camera scheme when the sphere pass throught this trigger
public class CameraSchemeSelector : MonoBehaviour
{
    [Header("1 = SE, SW, NW, NE / 2 = S, W, N, E / 3 = S, N / 4 = E, W")]
    [Range(1, 4)] public int schemeSelected;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == GameManager.sphereTag)
        {
            CameraRigHandler.doOnce = true;
            CameraRigHandler.camScheme = schemeSelected;
        }
    }
}
