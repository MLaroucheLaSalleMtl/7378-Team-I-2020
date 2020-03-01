using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] GameObject platform;

    
    private void Move()
    {
        platform.transform.TransformVector(0, 0, 0);
    }
}
