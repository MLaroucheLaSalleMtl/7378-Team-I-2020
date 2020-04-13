using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class DoorTrigger : MonoBehaviour
{
    private Animator anim;
    public bool test;
    private bool hasKeys;
    public string aiText;
    private bool doOnce;

    #region SFX 
    [Space]
    [Header("SFX")]
    private AudioSource sfx;
    [SerializeField] private AudioClip openSFX;
    [SerializeField] private AudioClip closeSFX;
    #endregion

    private void Start()
    {
        if (test) hasKeys = true;
        //else (hasKeys = GameManager.) to replace by the GameManager control of keys from others stages
        doOnce = false;
        anim = GetComponent<Animator>();
        sfx = GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (hasKeys)
        {
            if (other.gameObject.tag == GameManager.engineerTag)
            { anim.SetBool("isOpen", true); }
            sfx.PlayOneShot(openSFX);
        }
        if (!hasKeys)
        {
            if (!doOnce)
            {
                FindObjectOfType<AIUI>().ShowText(aiText);
            }
            doOnce = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == GameManager.engineerTag)
        { 
            anim.SetBool("isOpen", false);
            sfx.PlayOneShot(closeSFX);
        }

        doOnce = false;
    }
}
