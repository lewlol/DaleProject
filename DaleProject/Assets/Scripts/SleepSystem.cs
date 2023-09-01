using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepSystem : MonoBehaviour
{
    int dayCount;
    bool canSleep;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canSleep = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canSleep = false;
        }
    }

    private void Update()
    {
        if(canSleep && Input.GetKeyDown(KeyCode.Return))
        {
            NewDay();
        }
    }
    public void NewDay()
    {
        dayCount++;
        CustomEventSystem.current.Sleep();
    }
}
