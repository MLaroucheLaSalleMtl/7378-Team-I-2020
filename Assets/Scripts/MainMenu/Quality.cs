using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Quality : MonoBehaviour
{
    [SerializeField] private Text txtGFX; //Textfield to display the quality setting.
    private string[] GFXNames; //A list of all the preset's names
    private Slider slide;

    // Start is called before the first frame update
    void Start()
    {
        slide = GetComponent<Slider>(); //Retrieve the component Slider
        GFXNames = QualitySettings.names; //Retrieve the list of presets.
        float v = QualitySettings.GetQualityLevel(); //Retrieve the current Quality setting.
        slide.value = v; //Set the slider to right value
        txtGFX.text = GFXNames[(int)v]; //Display the right text.
    }

    public void SetGraphics(float val)
    {
        slide.value = val; //Set the slider to right value
        QualitySettings.SetQualityLevel((int)val, true); //Change the quality settings
        txtGFX.text = GFXNames[(int)val]; //Display the right text.
    }

    // Update is called once per frame
    void Update()
    {

    }
}


