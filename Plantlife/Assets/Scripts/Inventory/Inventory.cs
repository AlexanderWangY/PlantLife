using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<PlantItem> harvestedPlants = new List<PlantItem>();
    public int money = 10;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPlant(PlantItem plant)
    {
        harvestedPlants.Add(plant);
        Debug.Log($"Added {plant.itemName} to inventory. Total plants: {harvestedPlants.Count}");
    }

    public void SellAll()
    {
        foreach (var p in harvestedPlants)
        {
            money += p.sellValue;
            Debug.Log($"Sold {p.itemName} for {p.sellValue}. Total money: {money}");
        }

        harvestedPlants.Clear();
    }
}
