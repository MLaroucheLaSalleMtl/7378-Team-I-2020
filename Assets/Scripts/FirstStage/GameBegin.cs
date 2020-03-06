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
    [SerializeField] KillSpider spider;

    [Tooltip("Variable to skip intro if player wants")]
    public bool skipIntro;

    private CameraRigHandler camRig;

    private void Start()
    {
        skipIntro = false;
        engineerAnim = GameObject.FindGameObjectWithTag(GameManager.engineerTag).GetComponent<Animator>();
        sphereAnim = GameObject.FindGameObjectWithTag(GameManager.sphereTag).GetComponent<Animator>();
        zionAnim = GameObject.FindGameObjectWithTag(GameManager.zionTag).GetComponent<Animator>();
        spider = GameObject.FindObjectOfType<KillSpider>();
        camRig = GameObject.FindObjectOfType<CameraRigHandler>();
        
        PlayerSpawner.begin = true; //variable to control whether the spawner will show the initial instruction about the gaps
    }

    private void Update()
    {
        if (!skipIntro)
        {
            sphereAnim.SetBool("shutOff", true);
            if (zion) zionAnim.SetBool("walkAway", false);
            StartCoroutine(Begin(6f));
            GameManager.sphereOn = false;
        }
        else if (skipIntro)
        {
            engineerAnim.SetBool("shutOff", true);
            zionAnim.SetBool("walkAway", false);
            GameManager.engineerOn = true;
            GameManager.sphereOn = false;
        }
        if (Input.GetKeyDown(KeyCode.Backspace)) //TOBE removed - Just for testing purpose
        {
            GameManager.engineerOn = true;
            Destroy(zion);
            Destroy(zionLight);
            Destroy(this);
            GameManager.sphereOn = true;
            GameObject.FindObjectOfType<SwitchBridge>().OnClick();
            GameObject.FindObjectOfType<SwitchSphere>().OnClick();

            SwitchWall[] tempWall = GameObject.FindObjectsOfType<SwitchWall>();
            foreach (SwitchWall obj in tempWall)
            {
                obj.OnClick();
            }
            SwitchDoor[] temp = GameObject.FindObjectsOfType<SwitchDoor>();
            foreach (SwitchDoor obj in temp)
            {
                obj.OnOpen();
            }

            spider.Die();
            FindObjectOfType<AIInstructions>().beginTutorial = true;
        }
    }

    IEnumerator Begin(float time)
    {
        yield return new WaitForSeconds(time * 0.4f);
        yield return new WaitForSeconds(time * 0.4f);
        if (zion) zionAnim.SetBool("walkAway", true);
        yield return new WaitForSeconds(time * 0.1f);
        FindObjectOfType<AIInstructions>().beginTutorial = true;
        yield return new WaitForSeconds(time * 0.4f);
        Destroy(zionLight);
        Destroy(zion);
        GameManager.engineerOn = true;
        yield return new WaitForSeconds(time * 0.3f);
        if (camRig.index == 0) camRig.IndexChanger(+1);
        StopCoroutine("Begin");
        Destroy(this);
    }
}
