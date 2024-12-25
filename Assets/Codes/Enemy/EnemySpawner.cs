using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveNumber;
        public List<Enemy> enemies;
        public int waveQuota;
        public int spawnInterval;
        public int spawnCount;
    }

    [System.Serializable]
    public class Enemy
    {
        public string enemyName;
        public GameObject enemyPrefab;
        public int enemyCount;
        public int spawnCount;

    }
    public List<Wave> waves;
    public int currentWaveCount;
    [Header("Spawner Settings")]
    float spawnTimer;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CharacterStatus>().transform;
        CountWaveQuota();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemy();
        }
    }

    void CountWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemies in waves[currentWaveCount].enemies)
        {
            currentWaveQuota += enemies.enemyCount;
        }
        waves[currentWaveCount].waveQuota = currentWaveQuota;
    }

    void SpawnEnemy()
    {
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota)
        {
            foreach (var enemies in waves[currentWaveCount].enemies)
            {
                if (enemies.spawnCount < enemies.enemyCount)
                {
                    Vector3 spawnPosition = new Vector3(player.transform.position.x + Random.Range(-15f, 15f), player.transform.position.y, player.transform.position.z + Random.Range(-15f, 15f));
                    Instantiate(enemies.enemyPrefab, spawnPosition, Quaternion.identity);
                    enemies.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                }
            }
        }
    }
}
