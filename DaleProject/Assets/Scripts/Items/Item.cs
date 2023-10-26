using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [Header("Item Information")]
    public Sprite itemSprite;
    public string itemName;
    public string itemDescription;
    public Rarity itemRarity;

    [Header("Item Stats")]
    public float coolDownTime;
    public float activeTime;
    public bool infinite;

    public virtual void Activate(GameObject parent) { }

    public virtual void BeginCooldown(GameObject parent) { }
}
