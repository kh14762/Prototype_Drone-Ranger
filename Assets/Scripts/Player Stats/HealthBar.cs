using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Slider healthSlider; // health of the sojourner

    // set max health for sojourner
    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    // change health
    public void SetHealth(int health)
    {
        healthSlider.value = health;
    }


}
