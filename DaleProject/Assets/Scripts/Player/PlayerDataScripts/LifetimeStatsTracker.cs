using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifetimeStatsTracker : MonoBehaviour
{
    public PlayerLifetimeStatsData lifetimeStats;
    private void Start()
    {
        CustomEventSystem.current.onBlockBreak += TrackStats;
    }

    public void TrackStats(string blockName, TileTypes blockType)
    {
        switch (blockType) 
        {
            case TileTypes.Rock:
                lifetimeStats.totalrocksmined++;
                break;
            case TileTypes.Ore:
                lifetimeStats.totaloresmined++;
                break;
            case TileTypes.Gemstone:
                lifetimeStats.totalgemstonesmined++;
                break;
        }

        switch (blockName)
        {
            case "Stone":
                lifetimeStats.totalstone++;
                break;
            case "Greystone":
                lifetimeStats.totalgreystone++;
                break;
            case "Pinkstone":
                lifetimeStats.totalpinkstone++;
                break;
                //ADD ROCKS HERE 

            case "Coal":
                lifetimeStats.totalcoal++;
                break;
            case "Iron":
                lifetimeStats.totaliron++;
                break;
            case "Gold":
                lifetimeStats.totalgold++;
                break;
            case "Copper":
                lifetimeStats.totalcopper++;
                break;
            case "Silver":
                lifetimeStats.totalsilver++;
                break;
            case "Cobalt":
                lifetimeStats.totalcobalt++;
                break;
            case "Lapis Lazuli":
                lifetimeStats.totallapis++;
                break;
            case "Obsidian":
                lifetimeStats.totalobsidian++;
                break;
            case "Mithril":
                lifetimeStats.totalmithril++;
                break;
            case "Adamantite":
                lifetimeStats.totaladmantite++;
                break;
            //ADD ORES HERE

            case "Ruby":
                lifetimeStats.totalruby++;
                break;
            case "Sapphire":
                lifetimeStats.totalsapphire++;
                break;
            case "Jasper":
                lifetimeStats.totaljasper++;
                break;
            case "Emerald":
                lifetimeStats.totalemerald++;
                break;
            case "Amethyst":
                lifetimeStats.totalamethyst++;
                break;
            case "Moonstone":
                lifetimeStats.totalmoonstone++;
                break;
            case "Diamond":
                lifetimeStats.totaldiamond++;
                break;
            case "Tanzanite":
                lifetimeStats.totaltanzanite++;
                break;
            case "Jadeite":
                lifetimeStats.totaljadeite++;
                break;
            case "Quartz":
                lifetimeStats.totalquartz++;
                break;
            case "Black Onyx":
                lifetimeStats.totalblackonyx++;
                break;
            case "Citrine":
                lifetimeStats.totalcitrine++;
                break;
            case "Rose Quartz":
                lifetimeStats.totalrosequartz++;
                break;
                //ADD GEMSTONES HERE
        }

    }
}
