using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SellingUI : MonoBehaviour
{
    public TextMeshProUGUI rockSell;
    public TextMeshProUGUI oreSell;
    public TextMeshProUGUI gemSell;
    public TextMeshProUGUI totalSell;

    private void Start()
    {
        CustomEventSystem.current.onSellInventory += UpdateSellUI;
    }

    public void UpdateSellUI(int rocks, int ores, int gems, int total)
    {
        rockSell.text = rocks.ToString() + " Coins";
        oreSell.text = ores.ToString() + " Coins";
        gemSell.text = gems.ToString() + " Coins";
        totalSell.text = total.ToString() + " Coins";
    }
}
