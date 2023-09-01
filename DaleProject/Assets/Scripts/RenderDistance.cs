using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RenderDistance : MonoBehaviour
{
    GameObject[] obj;
    List<GameObject> tiles = new List<GameObject>();
    Camera cam;

    public void Start()
    {
        cam = Camera.main;
        CustomEventSystem.current.onWorldGenerated += GrabTiles;
    }

    public void GrabTiles()
    {
        obj = GameObject.FindGameObjectsWithTag("Block");
        foreach (var tile in obj)
        {
            tiles.Add(tile);
        }

        InvokeRepeating("RefreshRenderDistance", 0f, 5f);
    }
    public void RefreshRenderDistance()
    {
        while (true)
        {
            Debug.Log("RenderDistance");
            foreach (var tile in tiles)
            {
                if (tile == null)
                {
                    tiles.Remove(tile);
                    break;
                }
                float distance = Vector2.Distance(transform.position, tile.transform.position);
                if (distance <= cam.orthographicSize * 5)
                {
                    tile.SetActive(true);
                }
                else
                {
                    tile.SetActive(false);
                }
            }
        }
    }
}
