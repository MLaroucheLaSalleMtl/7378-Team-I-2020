using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//by Sidakpreet Singh & Sohyun Yi & Philipe Gouveia
 
public class MainMenu_Controller : MonoBehaviour
{
    //Sohyun Yi & Sidakpreet & Philipe
    internal static int levelToLoad = 1;
    private string levelselect;
    [SerializeField] private GameObject[] popUpMenus;

    public void Start_Game() //Sidakpreet
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void SelectLevelToLoad(string i) //Sidakpreet
    { //phil: changed to string to have it more reliable on the menu
        //levelselect = i;
        SceneManager.LoadScene(i);
    }

    
    public void OptionMenu() //Sohyun Yi
    {
        //bool state = optionMenu.activeSelf;
        //optionMenu.SetActive(!state);
        CloseMenus();
        popUpMenus[0].SetActive(!popUpMenus[0].activeSelf);
    }

    //public void CloseOptionMenu() //Sohyun Yi
    //{
    //    optionMenu.SetActive(false);
    //}

    public void LevelSelectMenu() //Phil
    {
        CloseMenus();
        popUpMenus[1].SetActive(!popUpMenus[1].activeSelf);
    }

    public void CreditsMenu() //Phil
    {
        CloseMenus();
        popUpMenus[2].SetActive(!popUpMenus[2].activeSelf);
    }

    public void ManualMenu() //Phil
    {
        CloseMenus();
        popUpMenus[3].SetActive(!popUpMenus[3].activeSelf);
    }

    public void QuitMenu() //Sohyun Yi
    {
        CloseMenus();
        popUpMenus[4].SetActive(!popUpMenus[4].activeSelf);
    }

    public void ExitGame() //Sohyun Yi
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
    }

    void Update() //Phil
    {
            if (Input.GetButtonDown("Cancel"))
            {
            CloseMenus();
            }
    }

    public void CloseMenus() //Phil
    {
        foreach (GameObject obj in popUpMenus)
        {
            obj.SetActive(false);
        }
    }
}
