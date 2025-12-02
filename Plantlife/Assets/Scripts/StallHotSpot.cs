using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit;

public class StallHotSpot : MonoBehaviour
{
    public bool isBuyZone = false;   // toggle in Inspector
    public SimpleStall stall;        // reference to main stall script

    public void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (isBuyZone)
            stall.BuyRandomSeed();
        else
            stall.SellAllCrops();
    }
}

