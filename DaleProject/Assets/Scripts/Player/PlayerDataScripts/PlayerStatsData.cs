using UnityEngine;

[CreateAssetMenu(fileName = "Player")]
public class PlayerStatsData : ScriptableObject
{
    [Header("Health / Stamina")]
    public int maxhealth;
    public int maxstamina;

    [Header("Player")]
    public float speed;
    public float charisma;  //Shop Discount (Rizz the Shopkeepers)
    public float helmetlight; //Starts at 0 goes up 0.5/1 every level tbd. USed in mining light script to make his torch light brighter

    [Header("Mining")]
    public float miningrange;   //Half a block at a time range
    public float miningspeed;   //in 0.1 seconds decreases
    public float stonefortune;//start at 1
    public float orefortune; //start at 1
    public float gemstonefortune;// start at 1
    public int stonebreakingpower;
    public int orebreakingpower;
    public int gemstonebreakingpower;

}