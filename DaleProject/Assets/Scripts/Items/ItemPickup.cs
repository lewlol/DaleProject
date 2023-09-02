using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public int itemAmount;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = item.itemSprite;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ItemHolder ih = collision.gameObject.GetComponent<ItemHolder>();
            //Check if any Active Slots
            if (ih.activeItem1 == null || ih.activeItem1 == item) //Active Slot 1
            {
                ih.activeItem1 = item;
                ih.item1Count += itemAmount;
                Destroy(gameObject);
            }
            else
            {
                if (ih.activeItem2 == null || ih.activeItem2 == item) //Active Slot 2
                {
                    ih.activeItem2 = item;
                    ih.item2Count += itemAmount;
                    Destroy(gameObject);
                }
                else
                {
                    if(ih.activeItem3 == null || ih.activeItem3 == item) //Active Slot 3
                    {
                        ih.activeItem3 = item;
                        ih.item3Count += itemAmount;
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    private void DeleteItem()
    {
        Destroy(gameObject);
    }
}
