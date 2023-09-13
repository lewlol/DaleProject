using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile")]
public class TileData : ScriptableObject
{
    [Header("Tile Information")]
    public string tileName;
    public string tileDescription;
    public TileTypes tileType;

    [Header("Tile Attributes")]
    public Sprite tileSprite;
    public int id;
    public float breakTime;
    public int sellPrice;
    public int breakingPower;
    public bool isTrigger;

    [Header("Ore Attributes")]
    public float veinChance;
    public int maxVeinCount;

    [Header("Crystal Attributes")]
    public Sprite upCrystal;
    public Sprite downCrystal;
    public Sprite leftCrystal;
    public Sprite rightCrystal;
}

