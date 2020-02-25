using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public static Transform target;
    public GameObject cameraRig;
    public float moveSpeed = 2.0f;

    private void Awake()
    {
        cameraRig = GameObject.FindGameObjectWithTag("CameraRig");
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, moveSpeed * Time.deltaTime);
        transform.LookAt(cameraRig.transform);
    }
}
