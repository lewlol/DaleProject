using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public SpriteRenderer sr;
    public void AssignStats(TileData td)
    {
        sr.sprite = td.tileSprite;
    }
}
