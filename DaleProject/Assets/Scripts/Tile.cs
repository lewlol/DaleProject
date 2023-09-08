using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public SpriteRenderer sr;
    public TileData tileDataHolder;
    public void Rock(TileData td)
    {
        tileDataHolder = td;
        sr.sprite = td.tileSprite;
    }

    public void Ore(TileData td)
    {
        tileDataHolder = td;
        sr.sprite = td.tileSprite;
    }

    public void Gemstone(TileData td, int x, int y, Texture2D CaveTexture)
    {
        tileDataHolder = td;

        //Find which side the block is on and then change the sprite
        if (CaveTexture.GetPixel(x + 1, y).r < 0.5) //Block on the Right Side
        {
            sr.sprite = tileDataHolder.rightCrystal;
        }
        else if (CaveTexture.GetPixel(x - 1, y).r < 0.5) //Block on the Left Side
        {
            sr.sprite = tileDataHolder.leftCrystal;
        }
        else if (CaveTexture.GetPixel(x, y + 1).r < 0.5) //Block on the Top
        {
            sr.sprite = tileDataHolder.upCrystal;
        }
        else if (CaveTexture.GetPixel(x, y - 1).r < 0.5) //Block on the Bottom
        {
            sr.sprite = tileDataHolder.downCrystal;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
