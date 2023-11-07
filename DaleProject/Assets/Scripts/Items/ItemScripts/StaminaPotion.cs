using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Stamina Potion")]
public class StaminaPotion : Item
{
    public int staminaGain;
    public override void Activate(GameObject parent)
    {
        parent.GetComponent<MiningScript>().AddStamina(staminaGain);
    }
}
