using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterMines : MonoBehaviour
{
    public Animator platformAnim;
    bool canActivate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canActivate = true;
        CustomEventSystem.current.IndicatorMessage("Press Enter to Ride to Mines", 0, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canActivate = false;
        CustomEventSystem.current.IndicatorMessage("", 0, true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && canActivate)
        {
            platformAnim.SetBool("Activated", true);
            StartCoroutine(TurnOffAnim());
        }
    }

    IEnumerator TurnOffAnim()
    {
        yield return new WaitForSeconds(15);
        platformAnim.SetBool("Activated", false);
    }
}
