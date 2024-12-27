using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "ScriptableObject/Character")]
public class CharacterScriptable : ScriptableObject
{
    [SerializeField]
    GameObject startingWeapon;
    public GameObject StartingWeapon {get => startingWeapon; private set => startingWeapon = value;}

    [SerializeField]
    float moveSpeed;
    public float MoveSpeed {get => moveSpeed; private set => moveSpeed = value;}
    [SerializeField]
    float sprintSpeed;
    public float SprintSpeed {get => sprintSpeed; private set => sprintSpeed = value;}

    [SerializeField]
    float healthPoint;
    public float HealthPoint {get => healthPoint; private set => healthPoint = value;}

    [SerializeField]
    float energyPoint;
    public float EnergyPoint {get => energyPoint; private set => energyPoint = value;}

    [SerializeField]
    float projectileSpeed;
    public float ProjectileSpeed {get => projectileSpeed; private set => projectileSpeed = value;}

    [SerializeField]
    float magnetRange;
    public float MagnetRange {get => magnetRange; private set => magnetRange = value;}
}
