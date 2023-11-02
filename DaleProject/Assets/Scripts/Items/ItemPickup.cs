using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public int itemAmount;
    public GameObject pressEText;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = item.itemSprite;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pressEText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                ItemHolder ih = collision.gameObject.GetComponent<ItemHolder>();
                //Check if any Active Slots
                if (ih.activeItem1 == null || ih.activeItem1 == item) //Active Slot 1
                {
                    ih.activeItem1 = item;
                    ih.item1Count += itemAmount;
                    CustomEventSystem.current.ItemPickup(item, itemAmount, 1);
                    Destroy(gameObject);
                }
                else
                {
                    if (ih.activeItem2 == null || ih.activeItem2 == item) //Active Slot 2
                    {
                        ih.activeItem2 = item;
                        ih.item2Count += itemAmount;
                        CustomEventSystem.current.ItemPickup(item, itemAmount, 2);
                        Destroy(gameObject);
                    }
                    else
                    {
                        if (ih.activeItem3 == null || ih.activeItem3 == item) //Active Slot 3
                        {
                            ih.activeItem3 = item;
                            ih.item3Count += itemAmount;
                            CustomEventSystem.current.ItemPickup(item, itemAmount, 3);
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pressEText.SetActive(false);
    }
}
