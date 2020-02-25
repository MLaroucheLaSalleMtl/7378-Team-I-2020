using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBegin : MonoBehaviour
{
    //script done to handle the animations at the beginning of the game

    [Header("Tag on Players Characters and Zion")]
    //Animator for the Players Characters and Zion to handle at the beginning of the game
    [SerializeField] private Animator engineerAnim;
    [SerializeField] private Animator sphereAnim;
    [SerializeField] private Animator zionAnim;
    [Space]
    [Header("Zion Game Object")]
    [SerializeField] GameObject zion;
    [SerializeField] GameObject zionLight;

    [Tooltip("Variable to skip intro if player wants")]
    public bool skipIntro;

    private void Start()
    {
        skipIntro = false;
        engineerAnim = GameObject.FindGameObjectWithTag(GameManager.engineerTag).GetComponent<Animator>();
        sphereAnim = GameObject.FindGameObjectWithTag(GameManager.sphereTag).GetComponent<Animator>();
        zionAnim = GameObject.FindGameObjectWithTag(GameManager.zionTag).GetComponent<Animator>();
    }

    private void Update()
    {
        if (!skipIntro)
        {
            engineerAnim.SetBool("shutOff", true);
            sphereAnim.SetBool("shutOff", true);
            zionAnim.SetBool("walkAway", false);
            StartCoroutine(Begin(10f));
            GameManager.sphereOn = false;
        }
        else if (skipIntro)
        {
            engineerAnim.SetBool("shutOff", false);
            GameManager.engineerOn = true;
            Destroy(zion);
            Destroy(zionLight);
            Destroy(this);
        }
        if (Input.GetKeyDown(KeyCode.Backspace)) //TOBE removed - Just for testing purpose
        {
            engineerAnim.SetBool("shutOff", false);
            GameManager.engineerOn = true;
            Destroy(zion);
            Destroy(zionLight);
            Destroy(this);
            GameManager.sphereOn = true; 
            GameObject.FindObjectOfType<SwitchBridge>().OnClick();
            GameObject.FindObjectOfType<SwitchSphere>().OnClick();
            GameObject.FindObjectOfType<SwitchDoor>().OnOpen();
        }
    }

    IEnumerator Begin(float time)
    {
        yield return new WaitForSeconds(time);
        engineerAnim.SetBool("shutOff", false);
        yield return new WaitForSeconds(10.0f);
        zionAnim.SetBool("walkAway", true);
        yield return new WaitForSeconds(5.0f);
        Destroy(zionLight);
        Destroy(zion);
        GameManager.engineerOn = true;
        StopCoroutine("Begin");
        Destroy(this);
    }
}
