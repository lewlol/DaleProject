using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemHolder : MonoBehaviour
{
    public Item activeItem1;
    public int item1Count;

    public Item activeItem2;
    public int item2Count;

    public Item activeItem3;
    public int item3Count;

    float item1CooldownTime;
    float item2CooldownTime;
    float item3CooldownTime;

    float item1ActiveTime;
    float item2ActiveTime;
    float item3ActiveTime;

    enum ItemState 
    { 
        Ready,
        Active,
        Cooldown
    }

    ItemState item1State = ItemState.Ready;
    ItemState item2State = ItemState.Ready;
    ItemState item3State = ItemState.Ready;

    private void Start()
    {
        CustomEventSystem.current.onItemPickup += ItemGained;
    }

    private void Update()
    {
        Item1Switch();
        Item2Switch();
        Item3Switch();
    }

    public void Item1Switch()
    {
        switch (item1State)
        {
            case ItemState.Ready:
                if (Input.GetKeyDown(KeyCode.F) && item1Count > 0)
                {
                    activeItem1.Activate(gameObject);
                    item1State = ItemState.Active;
                    item1ActiveTime = activeItem1.activeTime;

                    if(!activeItem1.infinite)
                        item1Count--;
                }
                break;
            case ItemState.Active:
                if (item1ActiveTime > 0)
                {
                    item1ActiveTime -= Time.deltaTime;
                }
                else
                {
                    if (item1Count <= 0)
                    {
                        activeItem1.BeginCooldown(gameObject);
                        activeItem1 = null;
                        item1State = ItemState.Ready;
                    }
                    else
                    {
                        activeItem1.BeginCooldown(gameObject);
                        item1State = ItemState.Cooldown;
                        item1CooldownTime = activeItem1.coolDownTime;
                    }
                }
                break;
            case ItemState.Cooldown:
                if (item1CooldownTime > 0)
                {
                    item1CooldownTime -= Time.deltaTime;
                }
                else
                {
                    item1State = ItemState.Ready;
                }
                break;
        }
    }
    public void Item2Switch()
    {
        switch (item2State)
        {
            case ItemState.Ready:
                if (Input.GetKeyDown(KeyCode.LeftShift) && item2Count > 0)
                {
                    activeItem2.Activate(gameObject);
                    item2State = ItemState.Active;
                    item2ActiveTime = activeItem2.activeTime;

                    if(!activeItem2.infinite)
                        item2Count--;
                }
                break;
            case ItemState.Active:
                if (item2ActiveTime > 0)
                {
                    item2ActiveTime -= Time.deltaTime;
                }
                else
                {
                    if (item2Count <= 0)
                    {
                        activeItem2.BeginCooldown(gameObject);
                        activeItem2 = null;
                        item2State = ItemState.Ready;
                    }
                    else
                    {
                        activeItem2.BeginCooldown(gameObject);
                        item2State = ItemState.Cooldown;
                        item2CooldownTime = activeItem2.coolDownTime;
                    }
                }
                break;
            case ItemState.Cooldown:
                if (item2CooldownTime > 0)
                {
                    item2CooldownTime -= Time.deltaTime;
                }
                else
                {
                    item2State = ItemState.Ready;
                }
                break;
        }
    }
    public void Item3Switch()
    {
        switch (item3State)
        {
            case ItemState.Ready:
                if (Input.GetKeyDown(KeyCode.Z) && item3Count > 0)
                {
                    activeItem3.Activate(gameObject);
                    item3State = ItemState.Active;
                    item3ActiveTime = activeItem3.activeTime;

                    if(!activeItem3.infinite)
                        item3Count--;
                }
                break;
            case ItemState.Active:
                if (item3ActiveTime > 0)
                {
                    item3ActiveTime -= Time.deltaTime;
                }
                else
                {
                    if (item3Count <= 0)
                    {
                        activeItem3.BeginCooldown(gameObject);
                        activeItem3 = null;
                        item3State = ItemState.Ready;
                    }
                    else
                    {
                        activeItem3.BeginCooldown(gameObject);
                        item3State = ItemState.Cooldown;
                        item3CooldownTime = activeItem3.coolDownTime;
                    }
                }
                break;
            case ItemState.Cooldown:
                if (item3CooldownTime > 0)
                {
                    item3CooldownTime -= Time.deltaTime;
                }
                else
                {
                    item3State = ItemState.Ready;
                }
                break;
        }
    }

    public void RemoveItem(float id)
    {
        switch (id) 
        {
            case 1:
                activeItem1 = null;
                item1Count = 0;
                CustomEventSystem.current.ItemUIUpdate("Item 2", null, 0, 1);
                break; 

            case 2:
                activeItem2 = null;
                item2Count = 0;
                CustomEventSystem.current.ItemUIUpdate("Item 2", null, 0, 2);
                break;

            case 3:
                activeItem3 = null;
                item3Count = 0;
                CustomEventSystem.current.ItemUIUpdate("Item 3",null, 0, 3);
                break;
        }
    }

    public void ItemGained(Item item, int itemAmount, int slot)
    {
        if (activeItem1 == null || activeItem1 == item) //Active Slot 1
        {
            activeItem1 = item;
            item1Count += itemAmount;
            CustomEventSystem.current.ItemUIUpdate(item.itemName,item.itemSprite, 0, 1);
        }
        else
        {
            if (activeItem2 == null || activeItem2 == item) //Active Slot 2
            {
                activeItem2 = item;
                item2Count += itemAmount;
                CustomEventSystem.current.ItemUIUpdate(item.itemName, item.itemSprite, 0, 2);
            }
            else
            {
                if (activeItem3 == null || activeItem3 == item) //Active Slot 3
                {
                    activeItem3 = item;
                    item3Count += itemAmount;
                    CustomEventSystem.current.ItemUIUpdate(item.itemName, item.itemSprite, 0, 3);
                }
            }
        }
    }
}
