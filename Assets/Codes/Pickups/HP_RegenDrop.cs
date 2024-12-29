using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_RegenDrop : Pickup, ICollectible
{
    public float lifetime = 30f;
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
        player.HP_Regen();
    }

    IEnumerator FlashBeforeDestruction()
    {
        yield return new WaitForSeconds(lifetime - 5);  

        Renderer renderer = GetComponent<Renderer>();
        float flashDuration = 5f;  
        float flashInterval = 0.2f;

        while (flashDuration > 0)
        {
            renderer.enabled = !renderer.enabled;  
            yield return new WaitForSeconds(flashInterval);
            flashDuration -= flashInterval;
        }

        renderer.enabled = true;  
    }
}
