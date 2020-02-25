using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    #region UI
    private Animation anim;
    [SerializeField] AnimationClip showAnim;
    [SerializeField] AnimationClip hideAnim;
    public bool isHidden;
    #endregion

    private void Awake()
    {
        anim = GetComponent<Animation>();
        isHidden = true;
        anim.clip = hideAnim;
        anim.Play();
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
            anim.clip = hideAnim;
            anim.Play();
            isHidden = true;
        }


    }

}
