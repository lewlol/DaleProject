using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Biome")]
public class BiomeData : ScriptableObject
{
    [Header("Biome Information")]
    public string biomeName;

    [Header("Biome Attributes")]
    public float biomeTemperature;
}
