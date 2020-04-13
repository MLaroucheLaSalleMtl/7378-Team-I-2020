using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

//by Sidakpreet Singh

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;   // to give the referenece to the audio we are using in this case using the audio mixer
    [SerializeField] private string nameParameter;


    Resolution[] resolutions = Screen.resolutions;

    public Dropdown resolutiondropdown;  // in order to add all our resolutions


    void Start()  
    {
        //for the volume
        Slider slide = GetComponent<Slider>();
        float v = PlayerPrefs.GetFloat(nameParameter, 0);
        slide.value = v;



        // in order to get all the resolutions we have on our screen available 
    
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
    public void SetVolume(float volume)
    {
        Slider slide = GetComponent<Slider>();
        audioMixer.SetFloat(nameParameter, volume);
        slide.value = volume;
        PlayerPrefs.SetFloat(nameParameter, volume);
        PlayerPrefs.Save();
    }

    //quality
    public void SetQuality(int qualityindex)  //0=low , 1= medium , 2= high
    {
        QualitySettings.SetQualityLevel(qualityindex);
    }

    //screen
    public void SetFullScreen(bool fullscreen)
    {
        Screen.fullScreen = !Screen.fullScreen;

    }
}