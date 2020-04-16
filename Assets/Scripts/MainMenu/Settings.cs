﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

//by Sidakpreet Singh


public class Settings : MonoBehaviour
{



    [SerializeField] private Slider audioMixerSlider; //phil
    [SerializeField] private AudioMixer audioMixer;   // to give the referenece to the audio we are using in this case using the audio mixer
    [SerializeField] private Text volValue;
    [SerializeField] private string nameParameter;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Text sfxValue;
    [SerializeField] private string nameParameterSfx; //phil
    


    Resolution[] resolutions;

    public Dropdown resolutiondropdown;  // in order to add all our resolutions


    void Start()  
    {
        //for the volume
        //Slider slide = GetComponent<Slider>(); //phil: commented in order to apply for the sfx as well
        float v = PlayerPrefs.GetFloat(nameParameter, 0); 
        audioMixerSlider.value = v;
        volValue.text = System.Convert.ToInt32(v + 80).ToString(); //phil

        float t = PlayerPrefs.GetFloat(nameParameterSfx, 0); //phil
        sfxSlider.value = t; //phil
        sfxValue.text = System.Convert.ToInt32(t + 80).ToString(); //phil


        // in order to get all the resolutions we have on our screen available 
        resolutions = Screen.resolutions;
        resolutiondropdown.ClearOptions();

        //adding our resultions using the string 

        List<string> options = new List<string>();

        int currrentresolutionindex = 0;

        for(int i=0; i<resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width
                && resolutions[i].height==Screen.currentResolution.height)
            {

                currrentresolutionindex = i;

            }
        }

        resolutiondropdown.AddOptions(options);  //resolution list will be added 

        resolutiondropdown.value = currrentresolutionindex;
        resolutiondropdown.RefreshShownValue();   // in order to display


    }

    //updating the resolution    
    public void  SetResolution(int resolutionindex)     
    {
        Resolution resolution = resolutions[resolutionindex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

    }

    //volume
    public void SetVolumeMusic(float volume)
    {
        Slider slide = audioMixerSlider;
        slide.value = volume;
        volValue.text = System.Convert.ToInt32(volume + 80).ToString();
        audioMixer.SetFloat(nameParameter, volume);
        PlayerPrefs.SetFloat(nameParameter, volume);
        PlayerPrefs.Save();
    }

    public void SetVolumeSFX(float volume)
    {
        Slider slide = sfxSlider;
        slide.value = volume;
        sfxValue.text = System.Convert.ToInt32(volume + 80).ToString();
        audioMixer.SetFloat(nameParameterSfx, volume);
        PlayerPrefs.SetFloat(nameParameterSfx, volume);
        PlayerPrefs.Save();
    }

    //screen
    public void SetFullScreen(bool fullscreen)
    {
        Screen.fullScreen = !Screen.fullScreen;

    }
}