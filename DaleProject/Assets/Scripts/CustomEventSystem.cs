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
}
