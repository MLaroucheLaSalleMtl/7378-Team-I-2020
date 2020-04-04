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

    //public bool endPuzzle; //tester

    private void Start()
    {
        bridge.transform.position += new Vector3(0, -1, 0);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameManager.engineerTag)
        {
            FindObjectOfType<CameraRigHandler>().stage_PlaceHolders[1] = cameraPlaceHolder;
            FindObjectOfType<LastStageManager>().isStage = !FindObjectOfType<LastStageManager>().isStage;
            FindObjectOfType<LastStageManager>().isTetris = !FindObjectOfType<LastStageManager>().isTetris;
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

        //if (endPuzzle)
        //{
        //    EndPuzzle();
        //}
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
