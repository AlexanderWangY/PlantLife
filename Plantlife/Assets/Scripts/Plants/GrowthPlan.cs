using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlantAction
{
    PLANT_SEED, // default for all plants, this starts every growth plan
    WATER,
    TILL,
    WAIT,
    PRUNE,
}


/// <summary>
/// An individual step in the growth plan
/// </summary>
[System.Serializable]
public class GrowthStep
{
    public PlantAction action;
}

/// <summary>
/// The events that will make a plant grow. Mostly tied to the usage of the tools
/// provided to the user, but could be any event.
/// </summary>
[CreateAssetMenu(menuName = "Plants/GrowthPlan")]
public class GrowthPlan : ScriptableObject
{
    public List<GrowthStep> steps;
}
