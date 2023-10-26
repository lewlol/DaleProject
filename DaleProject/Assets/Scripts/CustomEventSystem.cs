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

    public event Action onPlayerDamaged;
    public void PlayerDamaged()
    {
        if(onPlayerDamaged != null)
        {
            onPlayerDamaged();
        }
    }

    public event Action onCoinChange;
    public void CoinChange()
    {
        if(onCoinChange != null)
        {
            onCoinChange();
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
}
