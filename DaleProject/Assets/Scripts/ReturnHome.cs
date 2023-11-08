using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnHome : MonoBehaviour
{
    public Transform spawnPosition;
    public void ReturnToVillage()
    {
        GameObject player = GameObject.Find("Player");
        player.transform.position = spawnPosition.position;
        CustomEventSystem.current.HeadLampLight(false);
    }
}
