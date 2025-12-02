using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedButton : MonoBehaviour
{
    public SeedItem seed;
    public Transform spawnPoint;

    public void OnBuy()
    { 
     if (Inventory.instance.money >= seed.cost)
        {
            Inventory.instance.money -= seed.cost;
            Instantiate(seed.seedPrefab, spawnPoint.position, Quaternion.identity);
        }
     else
        {
            Debug.Log("Not enough money to buy " + seed.seedName);
        }
    }
}
