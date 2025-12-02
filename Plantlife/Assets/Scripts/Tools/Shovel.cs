using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : Tool
{
    // Start is called before the first frame update
    void Start()
    {

    }
    protected override void Update()
    {
        base.Update();
    }
    public override void Use()
    {
        Debug.Log("Shovel used");
        base.Use();
    }
}
