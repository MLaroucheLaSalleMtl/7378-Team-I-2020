﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch_Final : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private Animator animDoor;
    [SerializeField] private GameObject carPlayer;

    private void Start()
    {
        carPlayer = GameObject.FindGameObjectWithTag(GameManager.carTag);
        mat = GetComponent<Renderer>().material;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == GameManager.sphereTag)
        {
            if (Input.GetButton("Action4"))
            {
                OnClick();
            }
        }
    }

    public void OnClick()
    {
        mat.SetColor("_EmissionColor", Color.green);
        animDoor.SetBool("IsOpen", true);
        FindObjectOfType<AIUI>().ShowText("You can now use the car to explore the planet...");
        carPlayer.GetComponent<Rigidbody>().isKinematic = false;
    }
}
