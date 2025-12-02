using System.Collections;
using UnityEngine;
using System.Collections.Generic;
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
    public static int spawnedEnemyCount = 0;
    [SerializeField] private EnemyManager enemyManager;

    public WayPoints PathToUse;
    public int WaveIndex = 0;
    public int StartEnemyNumber = 0;

    void Awake()
    {

    }
    public void Start()
    {
        Debug.Log(WaveIndex);
    }
    void Update()
    {
        //Debug.Log(Time.time);

        //if (WaveIndex <= Waves.Length -1)
        //{
        //    Debug.Log("Wave terminer");
        //    return;
        //}


        if (spawnedEnemyCount > 0)
        {
            return;
        }



        countDown -= Time.deltaTime;

        if (countDown <= 0)
        {
            StartCoroutine(DoSpawn());
            WaveIndex++;
            countDown = timeBetweenWave;
        }
        
        /*if (WaveIndex >= Waves.Length)
        {
            Debug.Log("bravo terminée");
            return;
        }
        if (SpawnedEnemyCount > 0)
        {
            return;
        }
        if (CountDown <= 0)
        {
            StartCoroutine(DoSpawn());
            CountDown = TimeBetweenWave;
            return;
        }
        CountDown -= Time.deltaTime;*/
    }
    IEnumerator DoSpawn()
    {
        Wave wave = Waves[WaveIndex];
        //spawnedEnemyCount++;

        //int total = wave.enemies.Count;

        foreach (var enemyEntry in wave.enemies)
        {
            for (int i = 0; i < enemyEntry.count; i++) 
            {
               var chosen = wave.enemies[Random.Range(0, wave.enemies.Count)];
               var chosenGO = chosen.enemy;

                SpawnEnemy(chosenGO);
                //TimeBetweenEnemies = Random.Range(MinRange, MaxRange);


                yield return new WaitForSeconds(enemyEntry.rate); 
            }
        }

    }
    void SpawnEnemy(GameObject enemy) 
    { 
        Transform randomSpawn = spawnPoints[Random.Range(0,spawnPoints.Length)];  
        GameObject instance = Instantiate(enemy, randomSpawn.position, randomSpawn.rotation);


        WayPoints wp = randomSpawn.GetComponent<WayPoints>();
        instance.GetComponent<EnemyManager>().Path = wp;


        spawnedEnemyCount++;
        Debug.Log(spawnedEnemyCount);
    }
}



