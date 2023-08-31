using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLifetimeStats")]
public class PlayerLifetimeStatsData : ScriptableObject
{
    [Header("Coins")]
    public int coinsmade;
    public int coinsspent;
    [Header("Rocks")]
    public int totalrocksmined;
    //Add types of rock here
    [Header("Ores")]
    public int totaloresmined;
    //Add Ore types here
    [Header("Gemstones")]
    public int gemstonesmined;
    //Add gemstone types here
    [Header("Chests")]
    public int totalchestsfound;
    //can be changed depending how we do it
    public int commonchestsopened;
    public int uncommonchestsopened;
    public int rarechestsopened;
    public int epicchestsopened;
    public int legendarychestsopened;
}