using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SeedButton : MonoBehaviour
{
    public SeedItem seed;
    public Transform spawnPoint;
    public TMP_Text labelText;

    private void Start()
    {
        labelText.text = seed.seedName + ": $" + seed.cost.ToString();
    }

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
