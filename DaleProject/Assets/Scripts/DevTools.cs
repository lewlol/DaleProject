using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevTools : MonoBehaviour
{
    public GameObject[] UI;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            bool activeui = UI[0].gameObject.activeSelf;
            foreach (GameObject u in UI)
            {
                u.SetActive(!activeui);
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            player.GetComponent<MiningScript>().AddStamina(15);
        }
    }
}
