using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class TetrisHandler : MonoBehaviour
{
    [SerializeField] private Transform cameraPlaceHolder;
    [SerializeField] private TetrisPCSpawner[] controls;
    [SerializeField] private GameObject bridge;
    [SerializeField] private GameObject gap;
    [SerializeField] private GameObject box;
    [SerializeField] private GameObject switchReset;

    private Queue<string> talk = new Queue<string>();
    private float counter = 0;
    private float timeSpan = 200f;

    private void Start()
    {
        bridge.transform.position += new Vector3(0, -1, 0);

        talk.Enqueue("You need to cross to the other side of the bridge, but first you will need to build the bridge");
        talk.Enqueue("To do so, you must select the type of piece you want to use by placing the box on the corresponding switch");
        talk.Enqueue("You can switch cameras with the Character Switcher key");
        talk.Enqueue("Remember that you can always reset your build progress by holding the reset switch on the left side of the room");
        talk.Enqueue("# PIECES MOVEMENTS # \n - move - movement keys \n - flip =  Action 1  \n - place or replace = Action 4 \n - confirm the place = Action 3 \n                                  ");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameManager.engineerTag)
        {
            FindObjectOfType<CameraRigHandler>().stage_PlaceHolders[1] = cameraPlaceHolder;
            FindObjectOfType<LastStageManager>().isStage = !FindObjectOfType<LastStageManager>().isStage;
            FindObjectOfType<LastStageManager>().isTetris = !FindObjectOfType<LastStageManager>().isTetris;

            FindObjectOfType<AIUI>().ShowText(talk);
        }
    }

    private void Update()
    {
        if ((controls[0].pieceCounter >= 2) && (controls[1].pieceCounter >= 2) && (controls[2].pieceCounter >= 2))
        {
            if (PlaceChecker(controls[0]) && PlaceChecker(controls[1]) && PlaceChecker(controls[2]))
            {
                EndPuzzle();
            }
        }
    }

    private void LateUpdate()
    {
        if (FindObjectOfType<LastStageManager>().isTetris && counter >= timeSpan)
        {
            FindObjectOfType<AIUI>().ShowText("# PIECES MOVEMENTS # \n - move - movement keys \n - flip =  Action 1  \n - place or replace = Action 4 \n - confirm the place = Action 3 \n                                  ");
            counter = 0;
        }
        else if (FindObjectOfType<LastStageManager>().isTetris)
        {
            counter++;
        }
    }

    private void EndPuzzle()
    {
        gap.SetActive(false);
        bridge.transform.position += new Vector3(0, 1, 0);
        controls[0].gameObject.SetActive(false);
        controls[1].gameObject.SetActive(false);
        controls[2].gameObject.SetActive(false);
        box.SetActive(false);
        switchReset.SetActive(false);
        Destroy(this);
    }

    private bool PlaceChecker(TetrisPCSpawner control)
    {
        bool temp = true;

        foreach (GameObject pc in control.pieces)
        {
            if (pc.GetComponent<TetrisPCHandler>().inPlace == false) temp = false;
        }

        return temp;
    }
}
