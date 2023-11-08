using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DepthMeter : MonoBehaviour
{
    float depth;
    public GameObject player;
    public void Update()
    {
        Vector2 playerPos = new Vector2(0, player.transform.position.y);
        Vector2 ogPos = new Vector2(0, gameObject.transform.position.y);
        depth = Vector2.Distance(playerPos, ogPos);
        CustomEventSystem.current.DepthChange(depth);
    }
}
