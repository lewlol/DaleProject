using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public SpriteRenderer sr;
    public BoxCollider2D bx;
    public TileData tileDataHolder;
    public void Rock(TileData td)
    {
        tileDataHolder = td;
        sr.sprite = td.tileSprite;
        bx.isTrigger = td.isTrigger;
    }

    public void Ore(TileData td)
    {
        tileDataHolder = td;
        sr.sprite = td.tileSprite;
        bx.isTrigger = td.isTrigger;
    }

    public void Gemstone(TileData td, Sprite gemSprite)
    {
        tileDataHolder = td;
        sr.sprite = gemSprite;
        bx.isTrigger = td.isTrigger;
    }
}
