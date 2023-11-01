using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider healthUI;
    public TextMeshProUGUI healthText;

    private void Start()
    {
        CustomEventSystem.current.onHealthChange += HealthUIChange;
    }

    public void HealthUIChange(float health, float maxHealth)
    {
        healthUI.maxValue = maxHealth;
        healthUI.value = health;

        healthText.text = health + "/" + maxHealth;
    }
}
