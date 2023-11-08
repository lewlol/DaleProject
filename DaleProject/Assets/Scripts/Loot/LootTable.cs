using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Loot/LootTable")]
public class LootTable : ScriptableObject
{
    [Header("Loot Info")]
    public string lootName;

    [Header("Loot Chances")]
    [Range(0, 1)] public float coinChance;
    [Range(0, 1)] public float gemChance;
    [Range(0, 1)] public float itemChance;

    [Header("Loot Options")]
    public int minCoins;
    public int maxCoins;

    public TileData[] possibleGemstones;
    public int minGems;
    public int maxGems;

    public Item[] possibleItems;
    public int minItems;
    public int maxItems;
}
