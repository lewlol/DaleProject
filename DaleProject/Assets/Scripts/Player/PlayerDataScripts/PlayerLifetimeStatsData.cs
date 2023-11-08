using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLifetimeStats")]
public class PlayerLifetimeStatsData : ScriptableObject
{
    [Header("Coins")]
    public int coinsmade;
    public int coinsspent;
    [Header("Rocks")]
    public int totalrocksmined;
    public int totalstone;
    public int totalgreystone;
    public int totalpinkstone;
    //Add types of rock here
    [Header("Ores")]
    public int totaloresmined;
    public int totalcoal;
    public int totaliron;
    public int totalgold;
    public int totalcopper;
    public int totalsilver;
    public int totalcobalt;
    public int totallapis;
    public int totalobsidian;
    public int totalmithril;
    public int totaladmantite;


    //Add Ore types here
    [Header("Gemstones")]
    public int totalgemstonesmined;
    public int totalruby;
    public int totalsapphire;
    public int totaljasper;
    public int totalemerald;
    public int totalamethyst;
    public int totalmoonstone;
    public int totaldiamond;
    public int totaltanzanite;
    public int totaljadeite;
    public int totalquartz;
    public int totalblackonyx;
    public int totalcitrine;
    public int totalrosequartz;




    //Add gemstone types here
    [Header("Chests")]
    public int totalchestsfound;
    
    
}