using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBag : MonoBehaviour
{
    public int coins;

    public void AddCoins(int amount)
    {
        coins += amount;
        CustomEventSystem.current.CoinChange();
    }

    public void RemoveCoins(int amount)
    {
        coins -= amount;
        CustomEventSystem.current.CoinChange();
    }
}
