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
            if (Input.GetKey(KeyCode.E))
            {
                OnClick();
            }
        }
    }

    public void OnClick()
    {
        mat.SetColor("_EmissionColor", Color.green);
        bridge.GetComponent<Animator>().SetBool("liftBridge", true);
    }
}
