using UnityEngine;
using UnityEngine.AI;

public class EnemyLife : MonoBehaviour
{
    
    
   [SerializeField] private int EnemyTotalLife = 5;
    public GameObject DyingEffect;
    public string BulletTag = "Bullet";
    public string BaseTag = "Base";

    private bool IsDead = false;

    void Start()
    {
        

    }


    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsDead)
        {
            return;
        }
        if (other.tag == BulletTag)
        {
            EnemyTotalLife = EnemyTotalLife - 1;
            
            if (EnemyTotalLife <= 0)
            {

                EnemyDied();

            }

        }
        if (other.tag == BaseTag)
        {
            
            
            EnemyDied();
        }
    }

    public void EnemyDied()
    {
        IsDead = true;

        
        GameObject EffectINS = (GameObject)Instantiate(DyingEffect, transform.position, transform.rotation);
        Destroy(EffectINS, 2f);
        Destroy(gameObject);
        EnemySpawner.spawnedEnemyCount--;
        Debug.Log(EnemySpawner.spawnedEnemyCount);
    }
}
