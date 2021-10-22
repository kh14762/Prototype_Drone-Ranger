using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxTime(int time)
    {
        slider.maxValue = time;
        slider.value = time;
    }
   public void SetTime(int time)
    {
        slider.value = time;
    }
}
