using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] EngineerHandler engineerCharacter;
    [SerializeField] GameObject highlightEngineer;
    public static string engineerTag = "PlayerEngineer";
    public static string engineerImgTag = "PlayerEngineerImg";
    [SerializeField] SphereHandler sphereCharacter;
    [SerializeField] GameObject highlightSphere;
    public static string sphereTag = "PlayerSphere";
    public static string sphereImgTag = "PlayerSphereImg";
    [SerializeField] CarHandler carCharacter;
    [SerializeField] GameObject carHightlight;
    public static string carTag = "Car";
    #endregion

    public static string zionTag = "EnemyZion";

    //variables to control if the GameManager can change between players
    public static bool sphereOn;
    public static bool engineerOn;
    public float charChangeDistance = 15f;

    void Start()
    {
        charChangeDistance = 15f;
        engineerCharacter = GameObject.FindObjectOfType<EngineerHandler>();
        sphereCharacter = GameObject.FindObjectOfType<SphereHandler>();
        highlightEngineer = GameObject.FindGameObjectWithTag(engineerImgTag);
        highlightSphere = GameObject.FindGameObjectWithTag(sphereImgTag);

        Begin();
    }


    void Update()
    {
        if (Input.GetButtonUp("CharSwitcher") && sphereOn && engineerOn)
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

    private void Begin()
    {
        if (engineerCharacter)
        {
            engineerCharacter.engineerMove = true;
            if (sphereCharacter)
            {
                sphereCharacter.sphereMove = false;
            }
        }
        highlightEngineer.SetActive(true);
        highlightSphere.SetActive(false);
    }

    public void CharacterHandler()
    {
        engineerCharacter.engineerMove = !engineerCharacter.engineerMove;
        sphereCharacter.sphereMove = !sphereCharacter.sphereMove;
        if (engineerCharacter.engineerMove)
        {
            sphereCharacter.anim.SetBool("Open", false);
            sphereCharacter.bcol.enabled = false;

            highlightEngineer.SetActive(true);
            highlightSphere.SetActive(false);
        }
        else if (sphereCharacter.sphereMove)
        {
            highlightEngineer.SetActive(false);
            highlightSphere.SetActive(true);
        }
    }
}
