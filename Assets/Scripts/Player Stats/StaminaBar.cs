using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaSlider; // stamina of the sojourner

    // set max stamina for sojourner
    public void SetMaxStamina(int stamina)
    {
        staminaSlider.maxValue = stamina;
        staminaSlider.value = stamina;
    }

    // change stamina
    public void SetStamina(int stamina)
    {
        staminaSlider.value = stamina;
    }
}
