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
    public Rarity tileRarity;

    [Header("Tile Attributes")]
    public Sprite tileSprite;
    public int id;
    public float breakTime;
    public int sellPrice;
    public int breakingPower;
    public bool isTrigger;
    public bool isInventory;

    [Header("Ore Attributes")]
    public float veinChance;
    public int maxVeinCount;

    [Header("Crystal Attributes")]
    public Sprite upCrystal;
    public Sprite downCrystal;
    public Sprite leftCrystal;
    public Sprite rightCrystal;

    [Header("Loot Attributes")]
    public LootTable lootTable;

    [Header("Glow Attributes")]
    public bool glow;
    public float glowIntensity;
    public Color glowColor;
}

