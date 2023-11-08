using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    public List<TileData> tileInventory = new List<TileData>();

    int rockSellage;
    int oreSellage;
    int gemSellage;
    int totalSellage;

    public void AddResource(TileData tile, int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            Debug.Log(tile.tileName + " Added to Inventory");
            tileInventory.Add(tile);

            totalSellage += tile.sellPrice;
            switch (tile.tileType)
            {
                case TileTypes.Rock:
                    rockSellage += tile.sellPrice;
                    break;

                case TileTypes.Ore:
                    oreSellage += tile.sellPrice;
                    break;

                case TileTypes.Gemstone:
                    gemSellage += tile.sellPrice;
                    break;
            }
        }
    }

    public void SellBag()
    {
        tileInventory.Clear();
        CustomEventSystem.current.SellInventory(rockSellage, oreSellage, gemSellage, totalSellage);

        CustomEventSystem.current.AddCoins(totalSellage);

        rockSellage = 0;
        oreSellage = 0;
        gemSellage = 0;
        totalSellage = 0;

    }
}
