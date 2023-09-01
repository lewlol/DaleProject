using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Healing Potion")]
public class HealingPotionItem : Item
{
    public int healthGain;

    public override void Activate(GameObject parent)
    {
        parent.GetComponent<PlayerHealth>().AddHealth(healthGain);
        CustomEventSystem.current.TextDisplay(parent.transform.position, 1.5f, "+" + healthGain, 35, Color.red);
    }

    public override void BeginCooldown(GameObject parent)
    {
    }
}
