using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Regeneration Potion")]
public class RegenPotionItem : Item
{
    public int healthgain;
    public int regenTime;
    public override void Activate(GameObject parent)
    {
        parent.gameObject.GetComponent<PlayerHealth>().RegenerationEffect(healthgain, regenTime);
    }

    public override void BeginCooldown(GameObject parent)
    {
    }
}
