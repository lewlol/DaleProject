using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile")]
public class Tile : ScriptableObject
{
    [Header("Tile Information")]
    public string tileName;

    [Header("Tile Attributes")]
    public Sprite tileSprite;
}
