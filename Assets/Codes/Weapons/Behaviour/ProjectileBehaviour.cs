using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    protected Vector3 direction;
    public WeaponScriptable weaponStatus;
    public float destroyBulletTime;
    public CharacterStatus player;

    //current status
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldown;
    protected float currentSpecialCooldown;
    protected float currentPierce;

    bool isBuffed = false;

    void Awake()
{
    if (player == null)
    {
        player = FindObjectOfType<CharacterStatus>();  // Automatically find player if not assigned
    }

    // Apply player buff to the projectile
    currentDamage = weaponStatus.Damage * player.damageMultiplier;
    currentSpeed = weaponStatus.Speed;
    currentCooldown = weaponStatus.Cooldown;
    currentSpecialCooldown = weaponStatus.SpecialCooldown;
    currentPierce = weaponStatus.Pierce;
}
    protected virtual void Start()
    {
        Destroy(gameObject, destroyBulletTime);
    }

    // Update is called once per frame
    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyStats>().TakeDamage(currentDamage);
            currentPierce--;
            if (currentPierce <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void ScaleStatsByLevel()
    {
        if (player != null)
        {
            currentCooldown = 0.1f + weaponStatus.Cooldown * Mathf.Pow(0.95f, player.level);
        }
    }

    public void DamageBuff(float duration)
    {
        if (!isBuffed)
        {
            isBuffed = true;
            currentDamage = weaponStatus.Damage * 2;  // Apply damage buff
            StartCoroutine(DamageBuffTimer(duration));
        }
    }

    // Coroutine to revert damage after the buff duration
    IEnumerator DamageBuffTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        currentDamage = weaponStatus.Damage;
        isBuffed = false;  // Allow buff to be reapplied
    }

}
