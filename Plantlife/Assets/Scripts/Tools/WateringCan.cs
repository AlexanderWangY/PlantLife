using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : Tool
{
    public ParticleSystem waterParticles;
    // Start is called before the first frame update
    void Start()
    {
        waterParticles.Pause();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (toolUseTrigger.enabled)
        {
            waterParticles.Play();
            waterParticles.gameObject.SetActive(true);
        }
        else
        {
            waterParticles.Pause();
            waterParticles.gameObject.SetActive(false);
        }
    }
    public override void Use()
    {
        base.Use();
    }
}
