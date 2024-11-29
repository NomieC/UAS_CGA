using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
[Header("Weapon Stats")]
public GameObject prefab;
public float speed;
public float damage;
public float cooldown;
public float currentCooldown;
float coodlwonTimer = 0;
public int pierce;

protected HeroMovement hm;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        hm = FindObjectOfType<HeroMovement>();
        currentCooldown = cooldown;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        {
            Shoot();
        }
    }

    protected virtual void Shoot()
    {
        currentCooldown = cooldown;
    }
}
