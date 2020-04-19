using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Sohyun Yi
public class EnterSpaceShip : MonoBehaviour
{
    [SerializeField] SpiderAI[] SpiderAgent = null;
    private bombExplosion b;
    private SceneLoad s;
    public string stageName;
    private bool doOnce;
    private Queue<string> phare = new Queue<string>();

    // Start is called before the first frame update
    void Start()
    {
        doOnce = true;
        b = FindObjectOfType<bombExplosion>();
        phare.Enqueue($"Now you entered the {stageName} zone to move to next stage. But, you cannot go inside the spaceship.                        ");
        phare.Enqueue("There are spiders protecting it. If you go deeper inside, the spiders will follow and beleaguer you.                      ");
        phare.Enqueue("To use your bomb towards spiders, press the ACTION 4 button.                                     ");
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    private void OnTriggerStay(Collider other)
    {
        if (b.SpiderIsDead)
        {
            FindObjectOfType<AIUI>().ShowText("You have slained the spiders. Press ACTION 3 button to enter the spaceship.                             ");

            if (Input.GetButtonDown("Action4"))
            {
                Invoke("GoInsideShip", 2f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerCar")
        {

            Debug.Log(stageName);
            for (int i = 0; i < SpiderAgent.Length; i++)
            {
                SpiderAgent[i].PlayerEnter(other.transform);
            }

            FindObjectOfType<AIUI>().ShowText(phare);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerCar")
        {

            for (int i = 0; i < SpiderAgent.Length; i++)
            {
                SpiderAgent[i].PlayerExit(other.transform);
            }

        }
    }

    void GoInsideShip()
    {
        if (doOnce)
        {
            //s.BtnLoadScene(stageName);
            GameManager gm = GameManager.instance;
            doOnce = false;
            gm.LoadNewLevel(stageName);
            gm.ActivateNewScene();
        }
    }

}
