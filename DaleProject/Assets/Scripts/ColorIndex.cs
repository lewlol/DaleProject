using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Color Index")]
public class ColorIndex : ScriptableObject
{
    public Color common;
    public Color uncommon;
    public Color rare;
    public Color unique;
    public Color legendary;
    public Color mythic;
    public Color quest;
}
