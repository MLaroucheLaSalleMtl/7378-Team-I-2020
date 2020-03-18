using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MInimap : MonoBehaviour
{
    [SerializeField] private Transform FollowCar;
    [SerializeField] private GameObject carIcon;
    Vector3 offset;
    private float height = 650;

 void Start()
   {
        transform.position = FollowCar.position;
        offset = new Vector3(1, height, 1);
        //transform.SetParent(null);
    }

    private void Update()
    {
        carIcon.transform.position = FollowCar.position + new Vector3(1,600,1);
        carIcon.transform.parent = FollowCar.transform;
    }

    void LateUpdate()
    {
        if (FollowCar != null)
        {
            transform.position = FollowCar.position + offset;
                //Quaternion.LookRotation(-FollowCar.up, FollowCar.forward);
        }

    }
}
