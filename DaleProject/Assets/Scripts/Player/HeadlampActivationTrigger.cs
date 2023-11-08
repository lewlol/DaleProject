using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HeadlampActivationTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Light2D l = collision.gameObject.GetComponentInChildren<Light2D>();
            bool t = l.enabled;
            CustomEventSystem.current.HeadLampLight(!t);
        }
    }
}
