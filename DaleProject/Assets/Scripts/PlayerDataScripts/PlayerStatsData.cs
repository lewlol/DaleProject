using UnityEngine;

[CreateAssetMenu(fileName = "Player")]
public class PlayerStatsData : ScriptableObject
{
    public int maxhealth;
    public int maxstamina;
    public float speed;

    public float miningrange;   //Half a block at a time range
    public float fortune;       //TBD
    public int breakingpower;   //1,2,3,4
    public float miningspeed;   //in 0.1 seconds decreases
    public float shopdiscount;  //In percent

}