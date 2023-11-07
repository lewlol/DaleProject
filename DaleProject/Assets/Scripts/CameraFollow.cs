using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    Scene mines;
    Scene home;
    Scene activeScene;

    private void Start()
    {
        this.GetComponent<Camera>().backgroundColor = Color.black;

        mines = SceneManager.GetSceneByName("Cave");
        home = SceneManager.GetSceneByName("Village");
        activeScene = SceneManager.GetActiveScene();
    }
    private void LateUpdate()
    {
        if(mines == activeScene)
        {
            transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, 10, 90), player.transform.position.y, -10);
        }
        else
        {
            transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, -8, 25), player.transform.position.y, -10);
        }
    }
}
