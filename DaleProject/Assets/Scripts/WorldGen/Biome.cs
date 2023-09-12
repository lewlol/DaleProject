using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Biome")]
public class Biome : ScriptableObject
{
    [Header("Biome Information")]
    public string biomeName;

    [Header("Biome Settings")]
    [Range(0f, 0.1f)] public float caveFrequency; //Cave Size
    [Range(0f, 0.1f)] public float oreFrequency; //Ore Distribution
    [Range(0, 1f)] public float gemFrequency; //Gem Distribution
    [Range(0f, 1f)] public float biomeFrequency; //How Often this Biome Shows Up
    public bool hasOre;
    public bool hasGems;

    [Header("Biome Attributes")]
    public TileData rockTile;

    [Header("Biome Ores")]
    public TileData[] commonOres;
    public TileData[] rareOres;
    public TileData[] uniqueOres;

    [Header("Biome Gemstones")]
    public TileData[] commonGems;
    public TileData[] rareGems;
    public TileData[] uniqueGems;
}
