using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    internal static GameManager instance = null;
    void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }
    }
    #endregion

    #region Players
    [SerializeField] EngineerHandler engineerCharacter;
    [SerializeField] GameObject highlightEngineer;
    [SerializeField] SphereHandler sphereCharacter;
    [SerializeField] GameObject highlightSphere;
    #endregion

    [SerializeField] Camera mainCam;

    void Start()
    {
        Begin();
    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            CharacterHandler();
        }
    }

    private void Begin()
    {
        engineerCharacter.engineerMove = true;
        sphereCharacter.sphereMove = false;

        highlightEngineer.SetActive(true);
    }

    public void CharacterHandler()
    {
        engineerCharacter.engineerMove = !engineerCharacter.engineerMove;
        sphereCharacter.sphereMove = !sphereCharacter.sphereMove;
        if (engineerCharacter.engineerMove)
        {
            sphereCharacter.anim.SetBool("Open", false);
            sphereCharacter.bcol.enabled = false;
            sphereCharacter.rb.freezeRotation = false;
            
            highlightEngineer.SetActive(true);
            highlightSphere.SetActive(false);
            mainCam.transform.SetParent(engineerCharacter.transform);
        }
        else if (sphereCharacter.sphereMove)
        {
            highlightEngineer.SetActive(false);
            highlightSphere.SetActive(true);
            mainCam.transform.SetParent(sphereCharacter.transform);
        }
    }
}
