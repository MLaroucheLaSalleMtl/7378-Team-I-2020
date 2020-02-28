using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    #region UI
    private Animation anim;
    [Header("Show")]
    [SerializeField] AnimationClip showAnim;
    [Header("Hide")]
    [SerializeField] AnimationClip hideAnim;
    public bool isHidden;
    
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
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            HideBtn();
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
        configPanel.SetActive(!configPanel.activeSelf);
        if (configPanel.activeSelf) Time.timeScale = 0f;
        else Time.timeScale = 1f;
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
        yield return new WaitForSeconds(1.0f);
        saved.SetActive(false);
        StopCoroutine("Saved()");
    }
}
