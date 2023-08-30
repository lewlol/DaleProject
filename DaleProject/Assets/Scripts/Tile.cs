using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileData td;
    public SpriteRenderer blockSprite;

    private void TileStats()
    {
        if(td.tileSprite != null)
            blockSprite.sprite = td.tileSprite;
    }
}
