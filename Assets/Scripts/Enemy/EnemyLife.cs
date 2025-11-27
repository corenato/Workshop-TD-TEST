using UnityEngine;
using UnityEngine.AI;

public class EnemyLife : MonoBehaviour
{
    
    
    [SerializeField] private float EnemyTotalLife = 5f;
    public GameObject DyingEffect;
    public string BulletTag = "Bullet";


    void Start()
    {
        

    }


    private void Update()
    {
        if (EnemyTotalLife <= 0)
        {
            EnemyDied();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == BulletTag)
        {
            EnemyTotalLife = EnemyTotalLife - 1f;
            

        }
    }

    void EnemyDied()
    {
        GameObject EffectINS = (GameObject)Instantiate(DyingEffect, transform.position, transform.rotation);
        Destroy(EffectINS, 2f);
        EnemySpawner.SpawnedEnemyCount--;

        Destroy(gameObject);
    }
}
