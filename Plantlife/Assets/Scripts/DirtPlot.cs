using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtPlot : MonoBehaviour
{
    public GameObject tilledDirt;
    public GameObject untilledDirt;
    bool tilled = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!tilled && other.gameObject.tag.ToLower() == "till")
        {
            Instantiate(tilledDirt, transform);
            Destroy(untilledDirt);
            tilled = true;
            gameObject.tag = "tilledDirt";
        }
    }
}
