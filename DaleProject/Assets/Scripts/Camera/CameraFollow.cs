using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    private void Start()
    {
        this.GetComponent<Camera>().backgroundColor = Color.black;
    }
    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, 14, 85.5f), player.transform.position.y, -10);
    }
}
