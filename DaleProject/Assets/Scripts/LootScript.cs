using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript : MonoBehaviour
{
    public LootTable lootTable;

    public void AssignLootTable(LootTable lt)
    {
        lootTable = lt;
    }

    public void RunLoot()
    {
        //Try Coins
        int coinChance = Random.Range(0, 1);
        if(coinChance <= lootTable.coinChance)
        {
            int coinAmount = Random.Range(lootTable.minCoins, lootTable.maxCoins);
            CustomEventSystem.current.AddCoins(coinAmount);
            CustomEventSystem.current.TextDisplay(gameObject.transform.position, 3, "+" + coinAmount + " Coins", 35, Rarity.Legendary);
        }

        //Try Gems
        int gemChance = Random.Range(0, 1);
        if(gemChance <= lootTable.gemChance)
        {
            //Decide Gems
            int gem = Random.Range(0, lootTable.possibleGemstones.Length);
            TileData activeGem = lootTable.possibleGemstones[gem];
            int gemID = activeGem.id;

            //Decide Gem Amount
            int gemAmount = Random.Range(lootTable.minGems, lootTable.maxGems);

            CustomEventSystem.current.UpdateBackapck(gemAmount, gemID);
            CustomEventSystem.current.TextDisplay(gameObject.transform.position, 3, "+" + gemAmount + " " + activeGem.name, 35, activeGem.tileRarity);
        }

        //Try Items
    }
}
