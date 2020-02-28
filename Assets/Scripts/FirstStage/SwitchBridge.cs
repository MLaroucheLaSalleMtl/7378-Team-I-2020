using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBridge : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private string playerTag = GameManager.sphereTag;
    [SerializeField] private GameObject bridge;

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

    public void OnClick()
    {
        mat.SetColor("_EmissionColor", Color.green);
        bridge.GetComponent<Animator>().SetBool("liftBridge", true);
        FindObjectOfType<AIUI>().ShowText("<< _to_player: You can lift heavy objects with the __ENGINEER__ using the keyboard key [Q]. To drop them, press the same key again.>>");

    }
}
