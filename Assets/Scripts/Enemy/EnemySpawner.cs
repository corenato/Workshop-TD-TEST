using System.Collections;
using UnityEngine;
using UnityEngine.Jobs;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform EnemyPrefab;
    [SerializeField] private Transform Spawnpoint;
    [SerializeField] private float TimeBetweenWave = 5f;
    [SerializeField] private float CountDown = 2f;
    [SerializeField] private float SpawnedEnemyCount = 0f;       
    public WayPoints PathToUse;
    private float MaxEnemySpawn = 10f;

    public static int EnemyNumber;
    public int StartEnemyNumber = 0;

    public void Start()
    {
        EnemyNumber = StartEnemyNumber;
    }


    void Update()
    {

        if (SpawnedEnemyCount >= MaxEnemySpawn)
        {

            StopCoroutine("DoSpawn");


        }
        if (CountDown <= 0f && SpawnedEnemyCount < MaxEnemySpawn)
        {
            StartCoroutine("DoSpawn");
            CountDown = TimeBetweenWave;


        }


        CountDown -= Time.deltaTime;
    }

    IEnumerator DoSpawn()
    {
        Instantiate(EnemyPrefab, Spawnpoint.position, Spawnpoint.rotation);
        EnemyPrefab.GetComponent<EnemyMovement>().Path = PathToUse;
        SpawnedEnemyCount++;
        EnemyNumber++;

        yield return null;
    }
}
