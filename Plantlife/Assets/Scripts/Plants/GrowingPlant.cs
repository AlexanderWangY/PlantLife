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

    /// <summary>
    /// The plant item that this plant will yield when harvested.
    /// Also, the particle effect to play when harvested. (Can be null)
    /// </summary>
    public PlantItem plantItem;
    public ParticleSystem harvestFx;

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

    // it might be better to do this with raycasts, but I cannot be bothered
    /// <summary>
    /// A sphere that is able to capture interactions with tools.
    /// All plants need this to interact with the triggers that come from tools when they are used.
    /// </summary>
    public SphereCollider toolInteractor;

    void Start()
    {
        if(growthPlan.steps.Count != lifecycleStages.Length)
        {
            Debug.LogError("Growth plan and steps not equal on plant: " + name);
        }
    }

    void Update()
    {
        if (currentStageIndex < growthPlan.steps.Count && growthPlan.steps[currentStageIndex].action == PlantAction.WAIT)
        {
            StartCoroutine(GrowByWaiting());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with: " + collision.gameObject.name + " tag: " + collision.gameObject.tag);
        if (collision.gameObject.tag.ToLower() == "water")
        {
            PerformAction(PlantAction.WATER);
        }
        if(collision.gameObject.tag.ToLower() == "trim")
        {
            PerformAction(PlantAction.PRUNE);
        }
        if(collision.gameObject.tag.ToLower() == "till")
        {
            PerformAction(PlantAction.TILL);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger with: " + other.gameObject.name + " tag: " + other.gameObject.tag);
        if (other.gameObject.tag.ToLower() == "water")
        {
            PerformAction(PlantAction.WATER);
        }
        if (other.gameObject.tag.ToLower() == "prune")
        {
            PerformAction(PlantAction.PRUNE);
        }
        if (other.gameObject.tag.ToLower() == "till")
        {
            PerformAction(PlantAction.TILL);
        }
        if (other.gameObject.tag.ToLower() == "shovel")
        {
            Harvest();
        }
    }

    public void PerformAction(PlantAction action)
    {
        if (currentStageIndex >= growthPlan.steps.Count)
            return;

        GrowthStep step = growthPlan.steps[currentStageIndex];
        Debug.Log("Performing action: " + action + " on step: " + growthPlan.steps[currentStageIndex].action);

        if (action == PlantAction.PLANT_SEED)
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
        currentStageIndex++;
    }
    public void GrowByOneStage()
    {
        if(currentStageIndex <= lifecycleStages.Length - 1)
        {
            Destroy(currentStage); currentStage = null;
            currentStage = Instantiate(lifecycleStages[currentStageIndex], transform);
            currentStageIndex++;
        }
    }
    public IEnumerator GrowByWaiting()
    {
        yield return new WaitForSeconds(10f);
        PerformAction(PlantAction.WAIT);
    }

    public void Harvest()
    {
        if (currentStageIndex < lifecycleStages.Length)
        {
            Debug.LogWarning("Trying to harvest a plant that is not fully grown: " + name);
            return;
        }

        // Play harvesting particle effect
        if (harvestFx != null)
            Instantiate(harvestFx, transform.position, Quaternion.identity);

        Inventory.instance.AddPlant(plantItem);

        // If the plant is renewable, reset to the renewable stage
        if (isRenewable)
        {
            Destroy(currentStage); currentStage = null;
            currentStageIndex = renewableStageIndex;
            currentStage = Instantiate(lifecycleStages[currentStageIndex], transform);
            currentStageIndex++;
        }
        else
        {
            // Otherwise, destroy the plant
            Destroy(gameObject);
        }
    }
}
