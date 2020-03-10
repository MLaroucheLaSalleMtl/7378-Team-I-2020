﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSphere : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] ItemHighlight highlight;
    [SerializeField] GameObject nextHighlight;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == GameManager.engineerTag)
        {
            if (Input.GetButton("Action4"))
            {
                OnClick();
            }
        }
    }

    internal void OnClick()
    {
        if (highlight) highlight.blink = false;
        if (nextHighlight) nextHighlight.SetActive(true);
        FindObjectOfType<CameraRigHandler>().IndexChanger(+4);
        mat.SetColor("_EmissionColor", Color.green);
        GameManager.sphereOn = true;
        FindObjectOfType<AIUI>().ShowText("<< _to_player: You can control the engineer or the sphere by swapping between them with the keyboard key [TAB] / joystick [left stick click] ...\n The sphere can roll or walk. To switch its mode use keyboard key [Q].>>");
    }
}
