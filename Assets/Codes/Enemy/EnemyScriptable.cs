using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "ScriptableObject/Enemy")]
public class EnemyScriptable : ScriptableObject
{
    [SerializeField]
    float healthPoint;
    public float HealthPoint {get => healthPoint; private set => healthPoint = value;}

    [SerializeField]
    float damage;
    public float Damage {get => damage; private set => damage = value;}

    [SerializeField]
    float moveSpeed;
    public float MoveSpeed {get => moveSpeed; private set => moveSpeed = value;}
}
