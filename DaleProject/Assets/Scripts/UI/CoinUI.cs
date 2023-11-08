using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    public TextMeshProUGUI coinCount;

    private void Start()
    {
        CustomEventSystem.current.onUpdateCoins += CoinUIChange;
    }

    public void CoinUIChange(int amount)
    {
        coinCount.text = amount.ToString() + " Coins"; 
    }
}
