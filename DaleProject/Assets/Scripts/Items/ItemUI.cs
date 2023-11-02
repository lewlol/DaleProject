using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Image image1;
    public Image image2;
    public Image image3;

    public TextMeshProUGUI itemText1;
    public TextMeshProUGUI itemText2;
    public TextMeshProUGUI itemText3;
    void Start()
    {
        CustomEventSystem.current.onItemPickup += UpdateUI;
    }

    public void UpdateUI(Item item, int itemAmount, int itemSlot)
    {
        if(itemSlot == 1)
        {
            image1.sprite = item.itemSprite;
            itemText1.text = item.itemName;
        }else 
        if(itemSlot == 2)
        {
            image2.sprite = item.itemSprite;
            itemText2.text = item.itemName;
        }
        else 
        if(itemSlot == 3)
        {
            image3.sprite = item.itemSprite;
            itemText3.text = item.itemName;
        }
    }
}
