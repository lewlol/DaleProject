using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "World Area")]
public class WorldAreaData : ScriptableObject
{
    [Header("Area Information")]
    public string areaName;
    public int topLayerY;
    public int bottomLayerY;

    [Header("Area Attributes")]
    public TileData rockTile;
    public BiomeData[] commonBiomes;
    public BiomeData[] rareBiomes;

    [Header("Area Ores and Gems")]
    public TileData[] commonOres;
    public TileData[] rareOres;
    public TileData[] commonGems;
    public TileData[] rareGems;
}
