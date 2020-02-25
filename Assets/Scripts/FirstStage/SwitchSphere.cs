using System.Collections;
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
            if (Input.GetKey(KeyCode.E))
            {
                OnClick();
            }
        }
    }

    internal void OnClick()
    {
        mat.SetColor("_EmissionColor", Color.green);
        GameManager.sphereOn = true;
    }
}
