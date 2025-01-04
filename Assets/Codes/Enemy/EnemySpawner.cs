using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public float waveInterval;
    public List<Transform> spawnPoints;
    bool waveActive = false;
    Transform player;

    [Header("UI Settings")]
    public TextMeshProUGUI stagetext;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CharacterStatus>().transform;
        CountWaveQuota();
        UpdateStageDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (waves[currentWaveCount].spawnCount >= waves[currentWaveCount].waveQuota && !waveActive && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            StartCoroutine(NextWave());
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemy();
        }
    }

IEnumerator NextWave()
    {
        waveActive = true;
        yield return new WaitForSeconds(waveInterval);
        if (currentWaveCount < waves.Count - 1)
        {
            waveActive = false;
            currentWaveCount++;
            CountWaveQuota();
            UpdateStageDisplay();
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
                // Calculate random position around the player
                float randomAngle = Random.Range(0f, 360f);
                float randomDistance = Random.Range(35f, 45f);  // Adjust distance as needed
                Vector3 spawnOffset = new Vector3(
                    Mathf.Cos(randomAngle * Mathf.Deg2Rad) * randomDistance,
                    0,
                    Mathf.Sin(randomAngle * Mathf.Deg2Rad) * randomDistance
                );

                Vector3 spawnPosition = player.position + spawnOffset;

                // Instantiate enemy at the calculated position
                Instantiate(enemies.enemyPrefab, spawnPosition, Quaternion.identity);
                
                enemies.spawnCount++;
                waves[currentWaveCount].spawnCount++;

                // Break if the wave quota is filled
                if (waves[currentWaveCount].spawnCount >= waves[currentWaveCount].waveQuota)
                {
                    break;
                }
            }
        }
    }
}
void UpdateStageDisplay()
{
    stagetext.text = "Stage " + (currentWaveCount + 1).ToString();
}
}
