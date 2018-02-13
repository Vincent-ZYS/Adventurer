using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class InputNumber : MonoBehaviour {
    public Slider number_slider;
    public Text number_text;
    public InputField number_input;
    public static InputNumber instance;
    void Awake()
    {
        instance = this;
        number_text.text = "0";
    }
    public void SelectDropNumber()
    {
        number_text.text = ((int)(number_slider.value * 99.0f)).ToString();
        number_input.text = ((int)(number_slider.value * 99.0f)).ToString();
       
       


    }
}
