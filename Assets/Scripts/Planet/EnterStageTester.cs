using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sohyun Yi
public class EnterStageTester : MonoBehaviour
{
    private bool doOnce = true;
    public string sceneName;
    public string stageName;
    private Queue<string> phare = new Queue<string>();
    public bool isFirstStage;
    public bool noStage;

    [SerializeField] GameObject SpiderAgent;
    public bombExplosion b;

    [SerializeField] private GameObject spawnPlaceHolder; //phil
    #region SFX
    private AudioSource asour;
    [SerializeField] private AudioClip enterStageSound;
    #endregion

    private void Start()
    {
        asour = GetComponent<AudioSource>();
        doOnce = true;
        if (isFirstStage)
        {
            b = FindObjectOfType<bombExplosion>();
            //phare.Enqueue($"Now you entered the {stageName} zone to move to next stage. But, you cannot go inside the spaceship.                        ");
            phare.Enqueue($"Now are at the {stageName} zone. But, you cannot go further.                        ");
            phare.Enqueue("There are spiders protecting them. If you go deeper inside, the spiders will follow and beleaguer you.                      ");
            phare.Enqueue("To use your bomb towards spiders, press the PICK UP button (Q / X).                                     ");
        }
        else
        {
            phare.Enqueue($"Now you entered the {stageName} zone. press ACT ON button (E / Y) to enter the stage                         ");
        }
    }

    private void Update()
    {
        //if (spiderList.Count <= 0) canEnter = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameManager.carTag)
        {
            SpiderAgent.SetActive(true);
            if (!isFirstStage) b = FindObjectOfType<bombExplosion>();
            FindObjectOfType<AIUI>().ShowText(phare);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == GameManager.carTag)
        {
            if (b.SpiderIsDead)
            {
                FindObjectOfType<AIUI>().ShowText("You have slained the spiders. Press ACT ON (E / Y) button to enter the spaceship.                             ");
            }
            if (Input.GetButtonDown("Action4"))
            {
                if (!noStage)
                {
                    if (b.SpiderIsDead)
                    {
                        asour.PlayOneShot(enterStageSound);
                        Invoke("GoInsideShip", 2f);
                        PlanetHandler.spawnerPos = spawnPlaceHolder.transform;
                    }
                    else
                    {
                        FindObjectOfType<AIUI>().ShowText("You have to kill all spider. To use your bomb towards spiders, press the PICK UP button (Q / X).                                     ");
                    }
                }
                else if (noStage)
                {
                    FindObjectOfType<AIUI>().ShowText("This station is locked and empty. There is nothing to do here                                     ");
                }
            }
        }
    }


    void GoInsideShip()
    {
        if (doOnce)
        {
            doOnce = false;
            //s.BtnLoadScene(stageName);
            GameManager gm = GameManager.instance;
            doOnce = false;
            gm.LoadNewLevel(sceneName);
            gm.ActivateNewScene();
        }
    }
}
