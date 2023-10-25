using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    private void Start()
    {
        this.GetComponent<Camera>().backgroundColor = Color.black;
    }
    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, 10, 90), player.transform.position.y, -10);
    }
}
