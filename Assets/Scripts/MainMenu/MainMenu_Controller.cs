using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 
public class MainMenu_Controller : MonoBehaviour
{
    //Sohyun Yi and Sidakpreet
    int levelToLoad = 1;
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject quitMenu;

    public void Start_Game() //Sidakpreet
    {
        SceneManager.LoadScene(levelToLoad);
    }


    public void SelectLevelToLoad(int i) //Sidakpreet
    {
        levelToLoad = i;
        print(levelToLoad);
    }

    //Sohyun Yi
    public void OptionMenu()
    {
        //bool state = optionMenu.activeSelf;
        //optionMenu.SetActive(!state);
        optionMenu.SetActive(true);
    }

    public void CloseOptionMenu()
    {
        optionMenu.SetActive(false);
    }

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
