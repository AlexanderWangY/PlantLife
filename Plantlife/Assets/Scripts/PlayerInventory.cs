using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInventory : MonoBehaviour
{
    [Header("Money")]
    public int Money = 0;

    [Header("Item Storage")]
    public Dictionary<string, int> seeds = new Dictionary<string, int>();
    public Dictionary<string, int> crops = new Dictionary<string, int>();

    // ------------------------------
    // SEEDS
    // ------------------------------

    public void AddSeed(string seedId, int amount)
    {
        if (!seeds.ContainsKey(seedId))
            seeds[seedId] = 0;

        seeds[seedId] += amount;
    }

    public int GetSeedCount(string seedId)
    {
        return seeds.ContainsKey(seedId) ? seeds[seedId] : 0;
    }

    public bool RemoveSeed(string seedId, int amount)
    {
        if (!seeds.ContainsKey(seedId)) return false;
        if (seeds[seedId] < amount) return false;

        seeds[seedId] -= amount;
        return true;
    }

    // ------------------------------
    // CROPS
    // ------------------------------

    public void AddCrop(string cropId, int amount)
    {
        if (!crops.ContainsKey(cropId))
            crops[cropId] = 0;

        crops[cropId] += amount;
    }

    public int GetCropCount(string cropId)
    {
        return crops.ContainsKey(cropId) ? crops[cropId] : 0;
    }

    public bool RemoveCrop(string cropId, int amount)
    {
        if (!crops.ContainsKey(cropId)) return false;
        if (crops[cropId] < amount) return false;

        crops[cropId] -= amount;
        return true;
    }

    // ------------------------------
    // UTILITY
    // ------------------------------

    public int GetTotalCrops()
    {
        int total = 0;
        foreach (var kvp in crops)
            total += kvp.Value;

        return total;
    }

    public int GetTotalSeeds()
    {
        int total = 0;
        foreach (var kvp in seeds)
            total += kvp.Value;

        return total;
    }
}
