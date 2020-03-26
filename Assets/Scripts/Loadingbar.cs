using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

//by Sidakpreet Singh

public class Loadingbar : MonoBehaviour
{
    private AsyncOperation async;

    [SerializeField] private Image loadingbar;
    [SerializeField] private Text txt;


    [SerializeField] private int sceneToLoad = -1;  

    void Start()
    {
        Time.timeScale = 1.0f;
        Input.ResetInputAxes();
        Scene currentscene = SceneManager.GetActiveScene();
        System.GC.Collect();


        if (sceneToLoad < 0)  
        {
            async = SceneManager.LoadSceneAsync(currentscene.buildIndex + 1);
        }
        else
        {
            async = SceneManager.LoadSceneAsync(sceneToLoad);
        }
        async.allowSceneActivation = false;

    }
    

    // Update is called once per frame
    void Update()
    {
        if (loadingbar)  
        {
            loadingbar.fillAmount = async.progress + 0.1f;
        }
        if (txt)  
        {
            txt.text = ((async.progress + 0.1f) * 100).ToString("f2") + "%";
        }
        if (async.progress > 0.89f)  
        {
            async.allowSceneActivation = true;
        }
    }
}
