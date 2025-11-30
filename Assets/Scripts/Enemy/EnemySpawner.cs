using System.Collections;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.UIElements;
public class EnemySpawner : MonoBehaviour
{
    public Wave[] Waves;
    [SerializeField] private Transform[] SpawnPoints;
    //[SerializeField] private float TimeBetweenEnemies = 1f;
    [SerializeField] private float TimeBetweenWave = 2f;
    //[SerializeField] private float EnemyCountDown = 2f;
    [SerializeField] private float CountDown = 2f;
    //[SerializeField] private float MinRange = 0f;
    //[SerializeField] private float MaxRange = 5f;
    public static int SpawnedEnemyCount = 0;

    public WayPoints PathToUse;
    private int WaveIndex = 0;
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
        
        if (WaveIndex >= Waves.Length)
        {
            Debug.Log("Wave terminer");
            return;
        }
                

        if (SpawnedEnemyCount > 0)
        {
            return;
        }
                

        CountDown -= Time.deltaTime;

        if (CountDown <= 0)
        {
             StartCoroutine(DoSpawn());
             CountDown = TimeBetweenWave;
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

        int total = 0;
        foreach (var entry in wave.Enemies)
        { 
            total += entry.Count;
        }

        foreach (var enemyEntry in wave.Enemies)
        {
            for (int i = 0; i < enemyEntry.Count; i++) 
            { 
                SpawnEnemy(enemyEntry.Enemy); 
                //TimeBetweenEnemies = Random.Range(MinRange, MaxRange);
                yield return new WaitForSeconds(2f / enemyEntry.rate); 
            }
        }

        
        WaveIndex++;
    }
    void SpawnEnemy(GameObject enemy) 
    { 
        Transform randomSpawn = SpawnPoints[Random.Range(0,SpawnPoints.Length)];  
        GameObject instance = Instantiate(enemy, randomSpawn.position, randomSpawn.rotation);


        WayPoints wp = randomSpawn.GetComponent<WayPoints>();
        instance.GetComponent<EnemyMovement>().Path = wp;


        SpawnedEnemyCount++;
        Debug.Log(SpawnedEnemyCount);
    }
}



