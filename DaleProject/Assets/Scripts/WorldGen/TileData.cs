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
    public int id;
    public float breakTime;

    [Header("Ore Attributes")]
    public Sprite tileSprite;
    public float veinChance;
    public int maxVeinCount;

    [Header("Crystal Attributes")]
    public Sprite upCrystal;
    public Sprite downCrystal;
    public Sprite leftCrystal;
    public Sprite rightCrystal;
}

