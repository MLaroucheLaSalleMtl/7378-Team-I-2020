using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Controller : MonoBehaviour
{
    int levelToLoad = 1;
    [SerializeField] private GameObject optionMenu;

    public void Start_Game()
    {
        SceneManager.LoadScene(levelToLoad);
    }


    public void SelectLevelToLoad(int i)
    {
        levelToLoad = i;
        print(levelToLoad);
    }

    public void OptionMenu()
    {
        bool state = optionMenu.activeSelf;
        optionMenu.SetActive(!state);
        //optionMenu.SetActive(true);
    }

    //public void CloseOptionMenu()
    //{
    //    optionMenu.SetActive(false);
    //}

    void Update()
    {
        if (optionMenu.activeSelf)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                optionMenu.SetActive(false);
            }
        }
    }
}
