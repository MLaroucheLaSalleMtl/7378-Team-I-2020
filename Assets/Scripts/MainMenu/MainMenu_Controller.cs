using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Controller : MonoBehaviour
{
    int levelToLoad = 1;

    public void Start_Game()
    {
        SceneManager.LoadScene(levelToLoad);
    }


    public void SelectLevelToLoad(int i)
    {
        levelToLoad = i;
        print(levelToLoad);
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
