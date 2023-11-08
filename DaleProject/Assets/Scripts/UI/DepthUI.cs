using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DepthUI : MonoBehaviour
{
    public TextMeshProUGUI depthText;
    private void Start()
    {
        CustomEventSystem.current.onDepthChange += DepthUIChange;
    }
    public void DepthUIChange(float depth)
    {
        depthText.text = depth.ToString("F1") + "m Depth";
    }
}
