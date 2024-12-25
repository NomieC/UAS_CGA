using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Weapon")]
public class WeaponScriptable : ScriptableObject
{
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab {get => prefab; private set => prefab = value;}

    //basic Status for Weapon
    [SerializeField]
    float damage;
    public float Damage {get => damage; private set => damage = value;}

    [SerializeField]
    float speed;
    public float Speed {get => speed; private set => speed = value;}

    [SerializeField]
    float cooldown;
    public float Cooldown {get => cooldown; private set => cooldown = value;}

    [SerializeField]
    float specialCooldown;
    public float SpecialCooldown {get => specialCooldown; private set => specialCooldown = value;}

    [SerializeField]
    int pierce;
    public int Pierce {get => pierce; private set => pierce = value;}
}
