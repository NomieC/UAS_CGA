using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    protected Vector3 direction;
    public WeaponScriptable weaponStatus;
    public float destroyBulletTime;

    //current status
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldown;
    protected float currentSpecialCooldown;
    protected float currentPierce;

    void Awake()
    {
        currentDamage = weaponStatus.Damage;
        currentSpeed = weaponStatus.Speed;
        currentCooldown = weaponStatus.Cooldown;
        currentSpecialCooldown = weaponStatus.SpecialCooldown;
        currentPierce = weaponStatus.Pierce;
    }//Awake
    protected virtual void Start()
    {
        Destroy(gameObject, destroyBulletTime);
    }

    // Update is called once per frame
    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;
    }

    protected virtual void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Enemy"){
            other.GetComponent<EnemyStats>().TakeDamage(currentDamage);
            currentPierce--;
            if(currentPierce <= 0){
                Destroy(gameObject);
            }
        }
    }
}
