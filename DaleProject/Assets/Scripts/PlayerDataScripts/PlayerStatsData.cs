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

    [Header("Mining")]
    public float miningrange;   //Half a block at a time range
    public int breakingpower;   //1,2,3,4
    public float miningspeed;   //in 0.1 seconds decreases
    public float fortune;       //TBD
}