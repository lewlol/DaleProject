using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Tile : MonoBehaviour
{
    public SpriteRenderer sr;
    public BoxCollider2D bx;
    public GameObject glowLight;

    public TileData tileDataHolder;
    public void Rock(TileData td)
    {
        tileDataHolder = td;
        sr.sprite = td.tileSprite;
        bx.isTrigger = td.isTrigger;
        GlowAttributes(td);
    }

    public void Ore(TileData td)
    {
        tileDataHolder = td;
        sr.sprite = td.tileSprite;
        bx.isTrigger = td.isTrigger;
        GlowAttributes(td);
    }

    public void Gemstone(TileData td, Sprite gemSprite)
    {
        tileDataHolder = td;
        sr.sprite = gemSprite;
        bx.isTrigger = td.isTrigger;
        GlowAttributes(td);
    }

    public void GlowAttributes(TileData dt)
    {
        glowLight.SetActive(dt.glow);
        glowLight.GetComponent<Light2D>().intensity = dt.glowIntensity;
        glowLight.GetComponent<Light2D>().color = dt.glowColor;
    }
}
