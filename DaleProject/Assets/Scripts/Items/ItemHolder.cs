using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public Item item;
    float cooldownTime;
    float activeTime;

    enum ItemState 
    { 
        Ready,
        Active,
        Cooldown
    }

    ItemState state = ItemState.Ready;

    private void Update()
    {
        switch (state)
        {
            case ItemState.Ready:
                if (Input.GetKeyDown(KeyCode.F))
                {
                    item.Activate(gameObject);
                    state = ItemState.Active;
                    activeTime = item.activeTime;
                }
                break;
            case ItemState.Active:
                if(activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    item.BeginCooldown(gameObject);
                    state = ItemState.Cooldown;
                    cooldownTime = item.coolDownTime;
                }
                break;
            case ItemState.Cooldown:
                if(cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    state = ItemState.Ready;
                }
                break;
        }
    }
}
