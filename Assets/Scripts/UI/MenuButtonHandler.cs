using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButtonHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    private AudioSource _as;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip hoverClip;
    [SerializeField] private AudioClip clickClip;

    void Start()
    {
        _as = GetComponent<AudioSource>(); 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _as.PlayOneShot(clickClip);
        if (GetComponent<Button>() != null)
        {
            GetComponent<Button>().onClick.Invoke();
            Input.ResetInputAxes();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _as.PlayOneShot(hoverClip);
    }
}
