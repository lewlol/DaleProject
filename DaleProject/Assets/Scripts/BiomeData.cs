using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Biome")]
public class BiomeData : ScriptableObject
{
    [Header("Biome Information")]
    public string biomeName;

    [Header("Biome Tiles")]
    public TileData rockTile;

    [Header("Biome Attributes")]
    public float minTemperature;
    public float maxTemperature;
    public float minHumidity;
    public float maxHumidity;
    public int conditionPoints;
    public Color mapColour;
}
