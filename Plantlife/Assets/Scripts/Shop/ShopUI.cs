using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public TMP_Text moneyText;
    public TMP_Text plantCountText;

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "Money: $" + Inventory.instance.money.ToString();
        plantCountText.text = "Plants in basket: " + Inventory.instance.harvestedPlants.Count.ToString();
    }
}
