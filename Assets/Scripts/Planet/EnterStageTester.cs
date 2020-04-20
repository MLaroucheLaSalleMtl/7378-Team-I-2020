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

    //[SerializeField] SpiderAI[] SpiderAgent;
    private bombExplosion b;

    [SerializeField] private GameObject spawnPlaceHolder; //phil
    #region SFX
    private AudioSource asour;
    [SerializeField] private AudioClip enterStageSound;
    #endregion

    private void Start()
    {
        b = FindObjectOfType<bombExplosion>();

        asour = GetComponent<AudioSource>();
        doOnce = true;
        if (isFirstStage)
        {
            //phare.Enqueue($"Now you entered the {stageName} zone to move to next stage. But, you cannot go inside the spaceship.                        ");
            phare.Enqueue($"Now you entered the {stageName} zone. But, you cannot go further to other spaceships.                        ");
            phare.Enqueue("There are spiders protecting them. If you go deeper inside, the spiders will follow and beleaguer you.                      ");
            phare.Enqueue("To use your bomb towards spiders, press the ACTION 3 button.                                     ");
        }
        else
        {
            phare.Enqueue($"Now you entered the {stageName} zone. press ACTION 4 to enter the stage                         ");
        }
    }

    private void Update()
    {
        //if (spiderList.Count <= 0) canEnter = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameManager.carTag)
            FindObjectOfType<AIUI>().ShowText(phare);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == GameManager.carTag)
        {
            if (b.SpiderIsDead)
            {
                FindObjectOfType<AIUI>().ShowText("You have slained the spiders. Press ACTION 3 button to enter the spaceship.                             ");

                if (Input.GetButtonDown("Action4"))
                {
                    asour.PlayOneShot(enterStageSound);
                    Invoke("GoInsideShip", 2f);
                    PlanetHandler.spawnerPos = spawnPlaceHolder.transform;
                }
            }
            else
            {
                FindObjectOfType<AIUI>().ShowText("You have to kill all spider. To use your bomb towards spiders, press the ACTION 3 button.                                     ");
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
