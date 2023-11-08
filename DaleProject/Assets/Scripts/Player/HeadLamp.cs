using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HeadLamp : MonoBehaviour
{
    Light2D headLamp;

    private void Start()
    {
        headLamp = GetComponent<Light2D>();

        CustomEventSystem.current.onHeadLampLight += HeadLampActivate;
    }

    public void HeadLampActivate(bool activate)
    {
        if (activate)
        {
            headLamp.enabled = true;
        }
        else
        {
            headLamp.enabled = false;
        }
    }
}
