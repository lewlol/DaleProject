using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IndicatorMessage : MonoBehaviour
{
    public TextMeshProUGUI indicator;
    float textTime;
    string textMessage;

    private void Start()
    {
        CustomEventSystem.current.onIndicatorMessage += IndicatorMessageChange;
    }
    public void IndicatorMessageChange(string message, float time, bool forever)
    {
        textMessage = message;
        textTime = time;
        if (forever)
        {
            indicator.text = textMessage;
            return;
        }
        StartCoroutine(TextChange());
    }

    IEnumerator TextChange()
    {
        indicator.text = textMessage;
        yield return new WaitForSeconds(textTime);
        indicator.text = "";
    }
}
