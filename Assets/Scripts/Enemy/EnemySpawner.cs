using System.Collections;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    public Wave[] Waves;
    [SerializeField] private Transform Spawnpoint;
    [SerializeField] private float TimeBetweenWave = 2f;
    [SerializeField] private float CountDown = 2f;
    public static int SpawnedEnemyCount = 0;       
    public WayPoints PathToUse;
    private int WaveIndex = 0;

    public int StartEnemyNumber = 0;

    public void Start()
    {
        Debug.Log(WaveIndex);
    }


    void Update()
    {
            if (WaveIndex >= Waves.Length)
            {
                Debug.Log("bravo terminée");
                return;
            }

            if (SpawnedEnemyCount > 0)
            {
            return;
            }

            if(CountDown <= 0)
            {
                StartCoroutine(DoSpawn());
                CountDown = TimeBetweenWave;
            return;
            }

            
            CountDown -= Time.deltaTime;
    }

    IEnumerator DoSpawn()
    {
        Wave wave = Waves[WaveIndex];

        foreach (var enemyEntry in wave.Enemies)
        {
            for (int i = 0; i < enemyEntry.Count; i++)
            {

                SpawnEnemy(enemyEntry.Enemy);

                yield return new WaitForSeconds(1f / enemyEntry.rate);
            }
        }

        WaveIndex++;

        
    }


    void SpawnEnemy(GameObject enemy)
    {
            GameObject instance = Instantiate(enemy, Spawnpoint.position, Spawnpoint.rotation);
            instance.GetComponent<EnemyMovement>().Path = PathToUse;
        
            SpawnedEnemyCount++;
    }
    
}
