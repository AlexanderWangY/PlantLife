using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

/// <summary>
/// A growing plant is technically a container that holds one GameObject to represent the plant stage at a time.
/// When a plant has reached a new stage in its growth, this class will delete the old model that was used to 
/// represent it and instantiate the next stage in its growth.
/// </summary>
public class GrowingPlant : MonoBehaviour
{
    /// <summary>
    /// Array with values set in the editor for each stage to define how the plant grows. 
    /// A new stage is reached when some tool use is applied to the plant; plants can have
    /// as many stages in their lifecycle as they please.
    /// </summary>
    public GameObject[] lifecycleStages;
    public GameObject currentStage;
    public int currentStageIndex = 0;

    public GrowthPlan growthPlan;

    /// <summary>
    /// Lets a plant be able to regrow from an earlier stage instead of having to start from seed again.
    /// <br/>Ex: for something like a tomato plant, it might be nice to have its final stage be able to 
    /// "repeat".
    /// </summary>
    public bool isRenewable = false;

    /// <summary>
    /// If a stage is renewable, this index is where it starts back over from. If not, it automatically
    /// defaults to 0, so you don't need to worry about it.
    /// </summary>
    public int renewableStageIndex = 0;

    void Start()
    {
        if(growthPlan.steps.Count != lifecycleStages.Length)
        {
            Debug.LogError("Growth plan and steps not equal on plant: " + name);
        }
    }

    void Update()
    {

    }

    public void PerformAction(PlantAction action)
    {
        if (currentStageIndex >= growthPlan.steps.Count)
            return;

        GrowthStep step = growthPlan.steps[currentStageIndex];

        if(action == PlantAction.PLANT_SEED)
        {
            GrowFirstStage();
        }
        else if (action == step.action)
        {
            GrowByOneStage();
        }
    }

    public void GrowFirstStage()
    {
        currentStage = Instantiate(lifecycleStages[currentStageIndex], transform);
    }
    public void GrowByOneStage()
    {
        if(currentStageIndex < lifecycleStages.Length - 1)
        {
            Destroy(currentStage); currentStage = null;
            currentStageIndex++;
            currentStage = Instantiate(lifecycleStages[currentStageIndex], transform);
        }
    }
}
