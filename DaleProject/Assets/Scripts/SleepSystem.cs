using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepSystem : MonoBehaviour
{
    int dayCount;
    bool canSleep;
    Backpack playerBag;

    public GameObject SellUI;

    private void Start()
    {
        CustomEventSystem.current.onSleep += RemoveUI;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canSleep = true;
            playerBag = collision.gameObject.GetComponent<Backpack>();
            CustomEventSystem.current.IndicatorMessage("Press Enter to Sleep and Sell your Resources", 0, true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canSleep = false;
            CustomEventSystem.current.IndicatorMessage("", 0, true);
        }
    }

    private void Update()
    {
        if(canSleep && Input.GetKeyDown(KeyCode.Return))
        {
            playerBag.SellBag();
            SellUI.SetActive(!SellUI.activeSelf);
        }
    }

    public void RemoveUI()
    {
        SellUI.SetActive(false);
    }
    public void NewDay()
    {
        dayCount++;
        CustomEventSystem.current.Sleep();
    }
}
