using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRigHandler : MonoBehaviour
{
    #region Singleton
    public static CameraRigHandler instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }
    #endregion
    [Tooltip("index of the camera rig view inside the camPlaceHolder arrays")]
    [Range(0,3)] public int index; //index of the camera rig view inside the camPlaceHolder arrays
    [Tooltip("index of whole rig target position inside the stage")]
    public static int stageIndex; //index of whole rig target position inside the stage
    public bool isTopView; //in case we decide to use the back view as well
    public float moveSpeed = 2.0f; //camera move speed from one placeholder to another inside the rig
    public static bool doOnce;

    //schemes to control the camera used scheme 1 - SE, SW, NW, NE / 2 - S, W, N, E / 3 - S, N / 4 - E, W
    [Range(1,4)] public static int camScheme; //variable to be changed/controlled with triggers on the stage
    [Header("1 - SE, SW, NW, NE / 2 - S, W, N, E / 3 - S, N, S, N / 4 - E, W, E, W")]
    [SerializeField] Transform[] camPlaceHolder1;
    [SerializeField] Transform[] camPlaceHolder2;
    [SerializeField] Transform[] camPlaceHolder3;
    [SerializeField] Transform[] camPlaceHolder4;
    [Space]
    [Tooltip("Place holders for the Camera Rig to move across the stage")]
    [SerializeField] Transform[] stage_PlaceHolders;
    [Space]
    [SerializeField] Camera cam;

    private void Start()
    {
        index = 0;
        stageIndex = 0;
        isTopView = true;
        doOnce = true;
        camScheme = 1;
        CameraHandler.target = camPlaceHolder1[0];
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (isTopView)
        {
            switch (camScheme)
            {
                case 1:
                    {
                        TopViewHandler(camPlaceHolder1);
                    }
                    break;
                case 2:
                    {
                        TopViewHandler(camPlaceHolder2);
                    }
                    break;
                case 3:
                    {
                        TopViewHandler(camPlaceHolder3);
                    }
                    break;
                case 4:
                    {
                        TopViewHandler(camPlaceHolder4);
                    }
                    break;
                default:
                    {
                        TopViewHandler(camPlaceHolder1);
                    }
                    break;

            }
        }

        if (!isTopView) //insert code to change between cameras of the Sphere and Engineer when not in Top Down view
        {

        }

        if ((stageIndex >= 0) && (stageIndex < stage_PlaceHolders.Length))
        {
            transform.position = Vector3.Lerp(transform.position, stage_PlaceHolders[stageIndex].position, moveSpeed * Time.deltaTime);
        }
        else
        {
            if (stageIndex >= stage_PlaceHolders.Length) stageIndex = stage_PlaceHolders.Length - 1;
            if (stageIndex <= 0) stageIndex = 0;
        }
    }

    void TopViewHandler(Transform[] placeHolder)
    {
        if (doOnce)
        {
            CameraHandler.target = placeHolder[0];
            doOnce = false;
        }

        if (Input.GetButtonDown("CameraLeft"))
        {
            index++;
            if (index > 3) index = 0;

            CameraHandler.target = placeHolder[index];
        }
        if (Input.GetButtonDown("CameraRight"))
        {
            index--;
            if (index < 0) index = 3;

            CameraHandler.target = placeHolder[index];
        }
    }
}
