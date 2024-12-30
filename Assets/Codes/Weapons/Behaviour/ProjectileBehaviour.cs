using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    protected Vector3 direction;
    public WeaponScriptable weaponStatus;
    public float destroyBulletTime;
    public CharacterStatus player;

    public GameObject hitParticlePrefab;  // Reference to hit particle prefab

    // Current status
    public float currentDamage;
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

        // Apply player's damage multiplier to projectile
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

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Apply damage to the enemy
            other.GetComponent<EnemyStats>().TakeDamage(currentDamage);

            // Spawn hit particle at collision point
            SpawnHitParticle(transform.position);

            currentPierce--;
            if (currentPierce <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void SpawnHitParticle(Vector3 hitPosition)
    {
        if (hitParticlePrefab != null)
        {
            GameObject particleInstance = Instantiate(hitParticlePrefab, hitPosition, Quaternion.identity);
            Destroy(particleInstance, 2f);  // Destroy particle after 2 seconds
        }
    }

    public void ScaleStatsByLevel()
    {
        if (player != null)
        {
            currentPierce = weaponStatus.Pierce + (player.level/2);
            currentCooldown = 0.1f + weaponStatus.Cooldown * Mathf.Pow(0.5f, player.level);
        }
    }

    public void DamageBuff(float duration)
    {
        if (!isBuffed)
        {
            isBuffed = true;
            currentDamage = weaponStatus.Damage * 2;
            StartCoroutine(DamageBuffTimer(duration));
        }
    }

    IEnumerator DamageBuffTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        currentDamage = weaponStatus.Damage;
        isBuffed = false;
    }
}