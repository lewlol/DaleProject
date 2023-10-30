using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBag : MonoBehaviour
{
    public int coins;

    private void Start()
    {
        CustomEventSystem.current.onAddCoins += PlusCoins;
        CustomEventSystem.current.onRemoveCoins += RemoveCoins;
    }

    public void PlusCoins(int amount)
    {
        coins += amount;
        CustomEventSystem.current.UpdateCoins(amount);
    }

    public void RemoveCoins(int amount)
    {
        coins -= amount;
        CustomEventSystem.current.UpdateCoins(amount);
    }
}
