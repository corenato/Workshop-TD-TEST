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
        Debug.Log(WaveIndex);
    }
    void Update()
    {
        if (spawnedEnemyCount == deadEnemiesNumber)
        {
            countDown -= Time.deltaTime;

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

        // Spawn until the list is empty. Pick a random index each time.
        while (enemiesToSpawn.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, enemiesToSpawn.Count);
            GameObject enemyToSpawn = enemiesToSpawn[index];

            // Remove the chosen enemy from the list to avoid duplicates and to keep the loop correct
            enemiesToSpawn.RemoveAt(index);

            SpawnEnemy(enemyToSpawn);

            yield return new WaitForSeconds(1f);
        }
        // No need to Clear() — list should already be empty
    }
    void SpawnEnemy(GameObject enemy) 
    { 
        Transform randomSpawn = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];  
        GameObject instance = Instantiate(enemy, randomSpawn.position, randomSpawn.rotation);

        WayPoints wp = randomSpawn.GetComponent<WayPoints>();
        instance.GetComponent<EnemyManager>().Path = wp;

        spawnedEnemyCount++;
    }

    public static void DecreaseEnemyCount()
    {
        deadEnemiesNumber++;
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



