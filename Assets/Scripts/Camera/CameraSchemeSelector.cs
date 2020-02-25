using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSchemeSelector : MonoBehaviour
{
    [Header("1 = SE, SW, NW, NE / 2 = S, W, N, E / 3 = S, N / 4 = E, W")]
    [Range(1, 4)] public int schemeSelected;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == GameManager.sphereTag)
        {
            CameraRigHandler.camScheme = schemeSelected;
        }
    }
}
