using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SimpleStall : MonoBehaviour
{
    public PlayerInventory player;
    public string[] seedTypes;
    public int seedPrice = 10;
    public int cropPrice = 5;

    public void SellAllCrops()
    {
        int total = 0;

        foreach (var pair in player.crops)
            total += pair.Value * cropPrice;

        player.crops.Clear();
        player.Money += total;

        Debug.Log("Sold crops for " + total);
    }

    public void BuyRandomSeed()
    {
        if (player.Money < seedPrice)
        {
            Debug.Log("Not enough money!");
            return;
        }

        player.Money -= seedPrice;

        string seed = seedTypes[Random.Range(0, seedTypes.Length)];
        player.AddSeed(seed, 1);

        Debug.Log("Bought seed: " + seed);
    }
}
