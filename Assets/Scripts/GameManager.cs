using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//by Philipe Gouveia

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance = null;
    void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }
    }
    #endregion

    #region Players
    [SerializeField] internal EngineerHandler engineerCharacter;
    [SerializeField] GameObject uiEngineer;
    public static string engineerTag = "PlayerEngineer";
    public static string engineerUItag = "PlayerEngineerUI";
    public static string engineerName = "_ENGINEER_"; //engineer name to be used by others classes when sending instruction to the AI UI system 
    [SerializeField] internal SphereHandler sphereCharacter;
    [SerializeField] GameObject uiSphere;
    public static string sphereTag = "PlayerSphere";
    public static string sphereUItag = "PlayerSphereUI";
    public static string sphereName = "_SPHERE_"; //sphere name to be used by others classes when sending instruction to the AI UI system
    [SerializeField] internal CarHandler carCharacter; //to be used when the car player is implemented so the game manager can activate it
    [SerializeField] GameObject uiCar; //to be used when the car player is implemented so the game manager can activate it
    public static string carTag = "PlayerCar";
    public static string carUItag = "PlayerCarUI";
    #endregion

    [SerializeField] private GameObject uiAI;
    public static string aiTag = "AiUI";
    public static string zionTag = "EnemyZion";

    //variables to control if the GameManager can change between players
    public bool sphereOn;
    public bool engineerOn;
    public bool carOn;
    public bool mechOn;
    public float charChangeDistance = 15f;

    #region SceneManagement
    private AsyncOperation async;
    #endregion

    void Start()
    {
        charChangeDistance = 15f;
        engineerCharacter = FindObjectOfType<EngineerHandler>();
        sphereCharacter = FindObjectOfType<SphereHandler>();
        carCharacter = FindObjectOfType<CarHandler>();
        uiAI = GameObject.FindGameObjectWithTag(aiTag);
        uiEngineer = GameObject.FindGameObjectWithTag(engineerUItag);
        uiSphere = GameObject.FindGameObjectWithTag(sphereUItag);
        uiCar = GameObject.FindGameObjectWithTag(carUItag);

        Begin();
    }


    void Update()
    {
        if (carOn && FindObjectOfType<CarHandler>().carMove)
        {
            uiCar.SetActive(true);
            uiEngineer.SetActive(false);
            uiSphere.SetActive(false);
            uiAI.SetActive(false);
        }
        else
        {
            if ((sphereOn && engineerOn) && Input.GetButtonUp("CharSwitcher"))
            {
                if (Vector3.Magnitude(sphereCharacter.transform.position - engineerCharacter.transform.position) < charChangeDistance)
                {
                    CharacterHandler();
                }
                else
                {
                    FindObjectOfType<AIUI>().ShowText("Your link to the engineer is made of a radio frequency which is weak within this distance. Get closer to him.");
                }
            }
        }

        if (Input.GetButtonDown("Skip"))
        {
            AIUI.skip = true;
        }
    }

    private void Begin()
    {
        if (engineerCharacter)
        {
            engineerCharacter.engineerMove = true;
            if (sphereCharacter)
            {
                if (sphereCharacter) sphereCharacter.sphereMove = false;
                if (carCharacter) carCharacter.carMove = false;
            }

            uiEngineer.SetActive(true);
            uiSphere.SetActive(false);
            uiCar.SetActive(false);
        }
        else if (!engineerCharacter && sphereCharacter)
        {
            if (engineerCharacter) engineerCharacter.engineerMove = false;
            if (sphereCharacter) sphereCharacter.sphereMove = true;
            if (carCharacter) carCharacter.carMove = false;

            uiEngineer.SetActive(false);
            uiSphere.SetActive(true);
            uiCar.SetActive(false);
        }
        else if (!engineerCharacter && !sphereCharacter)
        {
            if (carCharacter) carCharacter.carMove = true;

            uiEngineer.SetActive(false);
            uiSphere.SetActive(false);
            uiCar.SetActive(true);
        }
    }

    public void CharacterHandler()
    {
        engineerCharacter.engineerMove = !engineerCharacter.engineerMove;
        sphereCharacter.sphereMove = !sphereCharacter.sphereMove;
        if (engineerCharacter.engineerMove)
        {
            sphereCharacter.anim.SetBool("Open", false);
            sphereCharacter.bcol.enabled = false;

            uiEngineer.SetActive(true);
            uiSphere.SetActive(false);
            uiCar.SetActive(false);
        }
        else if (sphereCharacter.sphereMove)
        {
            uiEngineer.SetActive(false);
            uiSphere.SetActive(true);
            uiCar.SetActive(false);
        }
    }

    public void ActivateNewScene()
    {
        async.allowSceneActivation = true;
    }

    public void LoadNewLevel(int sceneIndex)
    {
        async = SceneManager.LoadSceneAsync(sceneIndex);
        async.allowSceneActivation = false;
    }

    public void LoadNewLevel(string name)
    {
        async = SceneManager.LoadSceneAsync(name);
        async.allowSceneActivation = false;
    }
}
