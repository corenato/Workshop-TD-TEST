using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    public Wave[] Waves;
    [SerializeField] private Transform[] spawnPoints;
    //[SerializeField] private float TimeBetweenEnemies = 1f;
    [SerializeField] private float timeBetweenWave = 2f;
    //[SerializeField] private float EnemyCountDown = 2f;
    [SerializeField] private float countDown = 2f;
    //[SerializeField] private float MinRange = 0f;
    //[SerializeField] private float MaxRange = 5f;
    [SerializeField] public static int spawnedEnemyCount = 0;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] public WaveEnemyEntry waveEnemyEntry;
    [SerializeField] private List<GameObject> enemiesToSpawn;
    [SerializeField] private List<GameObject> enemiesSpawned;

    public CopperMine copperMine;
    [SerializeField] private GameObject cpMine;

    public WayPoints PathToUse;
    public int WaveIndex = 0;
    public int StartEnemyNumber = 0;
    public static int deadEnemiesNumber = 0;

    void Awake()
    {
        // Ensure the list is initialized (prevents null refs when generating spawns)
        if (enemiesToSpawn == null)
            enemiesToSpawn = new List<GameObject>();
    }
    public void Start()
    {
        
    }
    void Update()
    {
        if (enemiesToSpawn.Count == 0 && enemiesSpawned.Count == 0)
        {
            countDown -= Time.deltaTime;

            if(copperMine != null)
            {
                copperMine.ProduceResource();
            }

            if (countDown <= 0)
            {
                GenerateEnemyList();
                StartCoroutine(oneWave());
                WaveIndex++;
                countDown = timeBetweenWave;
            }
        }

    }
    IEnumerator oneWave()
    {
        // Safety: ensure WaveIndex is valid
        if (WaveIndex < 0 || WaveIndex >= Waves.Length)
            yield break;

        Wave wave = Waves[WaveIndex];

        // Calculate initial number of enemies (GenerateEnemyList has populated enemiesToSpawn)
        int totalEnemies = enemiesToSpawn.Count;

        // Guard against division by zero and negative/zero durations
        float spawnInterval = 0f;
        if (totalEnemies > 0 && wave.duration > 0f)
        {
            spawnInterval = wave.duration / totalEnemies;
            //Debug.Log(spawnInterval);
            //spawnInterval = Mathf.Max(0.01f, spawnInterval); // avoid zero / too small
        }

        // Choose allowed spawn points for this wave (fall back to global spawnPoints)
        Transform[] allowed = (wave.allowedSpawnPoints != null && wave.allowedSpawnPoints.Length > 0)
            ? wave.allowedSpawnPoints
            : spawnPoints;

        if (allowed == null || allowed.Length == 0)
        {
            yield break;
        }

        // Spawn until the list is empty. Pick a random index each time.
        while (enemiesToSpawn.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, enemiesToSpawn.Count);
            GameObject enemyToSpawn = enemiesToSpawn[index];

            // Remove the chosen enemy from the list to avoid duplicates and to keep the loop correct
            enemiesToSpawn.RemoveAt(index);

            SpawnEnemy(enemyToSpawn, allowed);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
    void SpawnEnemy(GameObject enemy, Transform[] allowedSpawnPoints)
    {
        // pick a random allowed spawn transform
        Transform randomSpawn = allowedSpawnPoints[UnityEngine.Random.Range(0, allowedSpawnPoints.Length)];
        GameObject instance = Instantiate(enemy, randomSpawn.position, randomSpawn.rotation);

        WayPoints wp = randomSpawn.GetComponent<WayPoints>();
        instance.GetComponent<EnemyManager>().enemySpawner = this;
        instance.GetComponent<EnemyManager>().Path = wp;

        enemiesSpawned.Add(instance);

        //spawnedEnemyCount++;
        Debug.Log("Spawned : " + spawnedEnemyCount);
    }

    public void DecreaseEnemyCount(GameObject enemy)
    {
        //deadEnemiesNumber++;
        enemiesSpawned.Remove(enemy);

        //if (enemiesToSpawn.Count <= 0 && deadEnemiesNumber >= spawnedEnemyCount)
        //{
        //    spawnedEnemyCount = 0;
        //    deadEnemiesNumber = 0;
        //    Debug.Log("Resetspawn " + spawnedEnemyCount);
        //    Debug.Log("Resetdead " + deadEnemiesNumber);
        //}
        //Debug.Log("Dead : " + deadEnemiesNumber);
    }

    private void GenerateEnemyList()
    {
        Wave wave = Waves[WaveIndex];

        for(int i = 0; i < wave.enemies.Count; i++)
        {
            for (int j = 0; j < wave.enemies[i].amount; j++)
            {
                enemiesToSpawn.Add(wave.enemies[i].enemy);
            }
        }
    }
}



