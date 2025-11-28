using UnityEngine;
using UnityEngine.AI;

public class EnemyLife : MonoBehaviour
{
    
    
   [SerializeField] private int EnemyTotalLife = 5;
    public GameObject DyingEffect;
    public string BulletTag = "Bullet";
    public string BaseTag = "Base";


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
            EnemyTotalLife = EnemyTotalLife - 1;
            

        }
        if (other.tag == BaseTag)
        {
            EnemyDied();
            EnemySpawner.SpawnedEnemyCount--;
        }
    }

    public void EnemyDied()
    {
        GameObject EffectINS = (GameObject)Instantiate(DyingEffect, transform.position, transform.rotation);
        Destroy(EffectINS, 2f);
        EnemySpawner.SpawnedEnemyCount--;
        Destroy(gameObject);
    }
}
