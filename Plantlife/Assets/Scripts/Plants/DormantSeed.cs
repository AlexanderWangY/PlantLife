using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DormantSeed : MonoBehaviour
{
    /// <summary>
    /// The plant that this seed will grow into. Grab one of the predefined ones in the _Prefabs/Plants folder
    /// for this.
    /// </summary>
    public GrowingPlant plantCycle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "tilledDirt" && collision.gameObject.GetComponent<TilledDirt>())
        {
            Plant(collision.gameObject.GetComponent<TilledDirt>().plantingPoint.position);
        }
    }

    public void Plant(Vector3 plantingPosition)
    {
        if (plantCycle == null)
        {
            Debug.Log("Plant cycle not set!");
            return;
        }
        GrowingPlant growingPlant = Instantiate(plantCycle, plantingPosition, Quaternion.identity);
        growingPlant.PerformAction(PlantAction.PLANT_SEED);
        Destroy(gameObject);
    }

}
