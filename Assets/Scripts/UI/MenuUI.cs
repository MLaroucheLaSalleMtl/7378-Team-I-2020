using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    #region UI
    private Animation anim;
    [Header("Show")]
    [SerializeField] AnimationClip showAnim;
    [Header("Hide")]
    [SerializeField] AnimationClip hideAnim;
    public bool isHidden;
    public int counter = 360;
    
    [Header("Menu Reaction Items")]
    [SerializeField] GameObject paused;
    [SerializeField] GameObject saved;
    [SerializeField] GameObject configPanel;
    #endregion

    private void Awake()
    {
        anim = GetComponent<Animation>();
        isHidden = true;
        anim.clip = hideAnim;
        anim.Play();
        counter = 360;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            HideBtn();
        }
        if (Input.GetButtonDown("Pause"))
        {
            Pause();
        }
        if (!isHidden)
        {
            counter--;
            if (counter < 0)
            {
                HideBtn();
            }
        }
    }

    public void HideBtn()
    {
        if (isHidden) //action to show the UI
        {
            anim.clip = showAnim;
            anim.Play();
            isHidden = false;
        }
        else //action to hide the UI
        {
            if (!paused.activeSelf)
            {
                anim.clip = hideAnim;
                anim.Play();
                isHidden = true;
                counter = 360;
            }
        }
    }

    public void Pause()
    {
        paused.SetActive(!paused.activeSelf);
        if (paused.activeSelf) Time.timeScale = 0f;
        else Time.timeScale = 1f;
    }

    public void Save()
    {
        StartCoroutine(Saved());
    }

    public void Config()
    {
        Debug.Log(configPanel.activeSelf);
        if (!configPanel.activeSelf)
        {
            Time.timeScale = 0f;
            configPanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            configPanel.SetActive(false);
        }
    }

    public void Restart() //included to restart stage from the beginning. Need to include later to restart from checkpoint
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    IEnumerator Saved()
    {
        saved.SetActive(true);
        //CALL SAVE METHOD FROM THE MAIN MENU
        yield return new WaitForSeconds(2.0f);
        saved.SetActive(false);
        
        StopCoroutine("Saved()");
    }
}
