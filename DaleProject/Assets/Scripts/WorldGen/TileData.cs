using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile")]
public class TileData : ScriptableObject
{
    [Header("Tile Information")]
    public TileDatabase tileName;
    public string tileDescription;
    public TileTypes tileType;

    [Header("Tile Attributes")]
    public Sprite tileSprite;
    public int id;
    public float breakTime;
}

