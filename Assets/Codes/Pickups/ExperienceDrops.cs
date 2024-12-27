using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceDrops : Pickup, ICollectible
{
    public int experienceValue;

    void Start()
    {
        Light light = GetComponentInChildren<Light>();
        if (light != null)
        {
            light.renderMode = LightRenderMode.ForcePixel;
        }
    }
    public void Collect()
    {
        CharacterStatus player = FindObjectOfType<CharacterStatus>();
        player.AddExperience(experienceValue);
    }

}
