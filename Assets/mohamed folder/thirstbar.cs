using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class thirstbar : MonoBehaviour
{

    public Slider slider;

    public void SetMaxthirst(float thirst)
    {
        slider.maxValue = thirst;
        slider.value = thirst;
    }

    // function to set the thirst on the slider
    public void SetThirst(float thirst)
    {
        slider.value = thirst;
    }



}
