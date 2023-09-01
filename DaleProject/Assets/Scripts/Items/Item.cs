using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public string itemName;
    public float coolDownTime;
    public float activeTime;

    public virtual void Activate(GameObject parent) { }

    public virtual void BeginCooldown(GameObject parent) { }
}
