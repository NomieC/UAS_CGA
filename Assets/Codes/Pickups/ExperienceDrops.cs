using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceDrops : MonoBehaviour, ICollectible
{
    public int experienceValue;

    public void Collect()
    {
        CharacterStatus player = FindObjectOfType<CharacterStatus>();
        player.AddExperience(experienceValue);
        Destroy(gameObject);
    }
}
