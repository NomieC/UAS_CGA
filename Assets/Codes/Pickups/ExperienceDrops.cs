using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceDrops : Pickup, ICollectible
{
    public int experienceValue;
    public float lifetime = 20f;
    void Start()
    {
        Light light = GetComponentInChildren<Light>();
        if (light != null)
        {
            light.renderMode = LightRenderMode.ForcePixel;
        }
        Destroy(gameObject, lifetime);
        StartCoroutine(FlashBeforeDestruction());
    }
    public void Collect()
    {
        CharacterStatus player = FindObjectOfType<CharacterStatus>();
        player.AddExperience(experienceValue);
    }

    IEnumerator FlashBeforeDestruction()
    {
        yield return new WaitForSeconds(lifetime - 10);  // Start flashing 5 sec before destruction

        Renderer renderer = GetComponent<Renderer>();
        float flashTime = 0.5f;
        while (flashTime > 0)
        {
            renderer.enabled = !renderer.enabled;
            yield return new WaitForSeconds(0.2f);
            flashTime -= 0.2f;
        }

        Destroy(gameObject);
    }
}
