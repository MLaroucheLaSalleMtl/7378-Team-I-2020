using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBridge : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private string playerTag = GameManager.sphereTag;
    [SerializeField] private GameObject bridge;

    [SerializeField] ItemHighlight highlight;
    [SerializeField] GameObject[] nextHighlight;

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
        if (highlight) highlight.blink = false;
        if (nextHighlight.Length > 0) foreach (GameObject obj in nextHighlight) obj.SetActive(true);
        mat.SetColor("_EmissionColor", Color.green);
        bridge.GetComponent<Animator>().SetBool("liftBridge", true);
        FindObjectOfType<AIUI>().ShowText($"<< _to_player: You can lift heavy objects with {GameManager.engineerName} using -> keyboard [Q] / Joystick [X]. To drop them, press the same key again.>>");

    }
}
