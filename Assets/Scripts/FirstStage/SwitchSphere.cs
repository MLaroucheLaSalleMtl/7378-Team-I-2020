﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSphere : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private string playerTag = GameManager.engineerTag;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
            if (Input.GetButton("Action4"))
            {
                OnClick();
            }
        }
    }

    internal void OnClick()
    {
        mat.SetColor("_EmissionColor", Color.green);
        GameManager.sphereOn = true;
        FindObjectOfType<AIUI>().ShowText("<< _to_player: You can control the engineer or the sphere by swapping between them with the keyboard key [TAB]... The sphere can roll or walk. To switch its mode use keyboard key [Q].>>");
    }
}
