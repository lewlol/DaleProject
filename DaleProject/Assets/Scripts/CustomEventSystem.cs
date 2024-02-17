using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEventSystem : MonoBehaviour
{
    public static CustomEventSystem current;

    private void Awake()
    {
        current = this;
    }

    public event Action<float, float> onHealthChange;
    public void HealthChange(float health, float maxHealth)
    {
        if(onHealthChange != null)
        {
            onHealthChange(health, maxHealth);
        }
    }

    public event Action<int, int> onStaminaChange;
    public void StaminaChange(int stamina, int maxStamina)
    {
        if(onStaminaChange != null)
        {
            onStaminaChange(stamina, maxStamina);
        }
    }

    public event Action<int> onAddCoins;
    public void AddCoins(int amount)
    {
        if(onAddCoins != null)
        {
            onAddCoins(amount);
        }
    }

    public event Action<int> onRemoveCoins;
    public void RemoveCoins(int amount)
    {
        if(onRemoveCoins != null)
        {
            onRemoveCoins(amount);
        }
    }

    public event Action<int> onUpdateCoins;
    public void UpdateCoins(int amount)
    {
        if(onUpdateCoins != null)
        {
            onUpdateCoins(amount);
        }
    }

    public event Action<int, int> onUpdateBackpack;
    public void UpdateBackapck(int amount, int id)
    {
        if(onUpdateBackpack != null)
        {
            onUpdateBackpack(amount, id);
        }
    }

    public event Action onWorldGenerated;
    public void WorldGenerated()
    {
        if(onWorldGenerated != null)
        {
            onWorldGenerated();
        }
    }

    public event Action<Vector3, float, string, int, Rarity> onTextDisplay;
    public void TextDisplay(Vector3 position, float time, string text, int size, Rarity rarity)
    {
        if(onTextDisplay != null)
        {
            onTextDisplay(position, time, text, size, rarity);
        }
    }

    public event Action onSleep;
    public void Sleep()
    {
        if(onSleep != null)
        {
            onSleep();
        }
    }

    public event Action<string, TileTypes> onBlockBreak;
    public void BlockBreak(string blockName, TileTypes blockType)
    {
        if(onBlockBreak != null)
        {
            onBlockBreak(blockName, blockType);
        }
    }

    public event Action<Item, int, int> onItemPickup;
    public void ItemPickup(Item item, int itemAmount, int itemSlot)
    {
        if(onItemPickup != null)
        {
            onItemPickup(item, itemAmount, itemSlot);
        }
    }

    public event Action<string, Sprite, int, int> onItemUIUpdate;
    public void ItemUIUpdate(string itemName, Sprite itemSprite, int itemAmount, int itemSlot)
    {
        if(onItemUIUpdate != null)
        {
            onItemUIUpdate(itemName, itemSprite, itemAmount, itemSlot);
        }
    }

    public event Action<bool> onHeadLampLight;
    public void HeadLampLight(bool activate)
    {
        if (onHeadLampLight != null)
        {
            onHeadLampLight(activate);
        }
    }

    public event Action<int, int, int, int> onSellInventory;
    public void SellInventory(int rocks, int ores, int gems, int total)
    {
        if (onSellInventory != null)
        {
            onSellInventory(rocks, ores, gems, total);
        }
    }

    public event Action<float> onDepthChange;
    public void DepthChange(float depth)
    {
        if (onDepthChange != null)
        {
            onDepthChange(depth);
        }
    }

    public event Action<string, float, bool> onIndicatorMessage;
    public void IndicatorMessage(string message, float time, bool forever)
    {
        if(onIndicatorMessage != null)
        {
            onIndicatorMessage(message, time, forever);
        }
    }
}
