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

    public TextMeshProUGUI itemCount1;
    public TextMeshProUGUI itemCount2;
    public TextMeshProUGUI itemCount3;

    public Sprite empty;
    void Start()
    {
        CustomEventSystem.current.onItemUIUpdate += UpdateUI;
    }

    public void UpdateUI(string itemName, Sprite itemSprite, int itemAmount, int itemSlot)
    {

        if(itemSprite == null)
        {
            if (itemSlot == 1)
            {
                image1.sprite = empty;
                itemText1.text = "None";
                itemCount1.text = "";
            }
            else
            if (itemSlot == 2)
            {
                image2.sprite = empty;
                itemText2.text = "None";
                itemCount2.text = "";
            }
            else
            if (itemSlot == 3)
            {
                image3.sprite = empty;
                itemText3.text = "None";
                itemCount3.text = "";
            }
            return;
        }

        if(itemSlot == 1)
        {
            image1.sprite = itemSprite;
            itemText1.text = itemName;
            itemCount1.text = itemAmount.ToString();
        }else 
        if(itemSlot == 2)
        {
            image2.sprite = itemSprite;
            itemText2.text = itemName;
            itemCount2.text = itemAmount.ToString();
        }
        else 
        if(itemSlot == 3)
        {
            image3.sprite = itemSprite;
            itemText3.text = itemName;
            itemCount3.text = itemAmount.ToString();
        }
    }
}
