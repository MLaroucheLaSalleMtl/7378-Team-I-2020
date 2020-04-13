using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//by Philipe Gouveia & Sidakpreet Singh

public class MenuButtonHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IDeselectHandler, IPointerDownHandler
{
    private AudioSource asource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip hoverClip;
    [SerializeField] private AudioClip clickClip;

    void Start() //by Philipe Gouveia
    {
        asource = GetComponent<AudioSource>(); 
    }

    public void OnPointerClick(PointerEventData eventData) //by Philipe Gouveia
    {
        asource.PlayOneShot(clickClip);
        if (GetComponent<Button>() != null)
        {
            GetComponent<Button>().onClick.Invoke();
            Input.ResetInputAxes();
        }
    }

    public void OnPointerEnter(PointerEventData eventData) //by Sidakpreet Singh
    {
        asource.PlayOneShot(hoverClip);
        GetComponent<Selectable>().Select();
    }

    public void OnPointerDown(PointerEventData eventData) //by Sidakpreet Singh
    {
        if (GetComponent<Button>() != null)
        {
            GetComponent<Button>().onClick.Invoke();
            Input.ResetInputAxes();
        }
    }

    public void OnDeselect(BaseEventData eventData) //by Sidakpreet Singh
    {
        GetComponent<Selectable>().OnPointerExit(null);
    }
}
