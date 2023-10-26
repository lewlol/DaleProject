using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    public Slider stamUI;

    private void Start()
    {
        CustomEventSystem.current.onStaminaChange += StaminaUIChange;
    }
    public void StaminaUIChange(int stamina, int maxStamina)
    {
        stamUI.maxValue = maxStamina;
        stamUI.value = stamina;
    }
}
