using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class PortalToMaze : MonoBehaviour
{
    [SerializeField] private GameObject portal;
    [SerializeField] private Portal control;

    [SerializeField] private GameObject[] deactivateObjects;
    [SerializeField] private GameObject[] actiaveteObjects;

    [SerializeField] private Transform cameraRigTarget;
    [SerializeField] private Transform mazePlayer;

    private void Start()
    {
        portal.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == GameManager.sphereTag)
        {
            if (Input.GetButtonDown("Action4"))
            {
                control.isActive = true;
                portal.SetActive(true);

                Invoke("TeleportToMaze", 3f);
                //ChangeToEngineer();
            }
        }
    }

    private void ChangeToEngineer()
    {
        CameraRigHandler.stageIndex = 0;
        FindObjectOfType<CameraRigHandler>().stage_PlaceHolders[1] = FindObjectOfType<CameraRigHandler>().stage_PlaceHolders[0];
        FindObjectOfType<GameManager>().CharacterHandler();
    }

    private void TeleportToMaze()
    {
        foreach(GameObject obj in deactivateObjects)
        {
            obj.SetActive(false);
        }
        foreach(GameObject obj in actiaveteObjects)
        {   
            obj.SetActive(true);
        }

        FindObjectOfType<GameManager>().carOn = false;
        FindObjectOfType<GameManager>().engineerOn = false;
        FindObjectOfType<GameManager>().sphereOn = false;

        CameraRigHandler.stageIndex = 0;
        FindObjectOfType<CameraRigHandler>().stage_PlaceHolders[0] = mazePlayer;
        FindObjectOfType<CameraRigHandler>().stage_PlaceHolders[1] = cameraRigTarget;
        FindObjectOfType<CameraRigHandler>().camScheme = 1;

        FindObjectOfType<CameraRigHandler>().hasEngineer = false;
        FindObjectOfType<CameraRigHandler>().hasSphere = false;

        FindObjectOfType<LastStageManager>().isMaze = true;
        FindObjectOfType<LastStageManager>().isStage = false;
        FindObjectOfType<LastStageManager>().isTetris = false;
    }
}
