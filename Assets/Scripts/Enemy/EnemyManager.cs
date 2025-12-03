using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    
    
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currentHealth;
    [SerializeField] private GameObject mainBase;
    public GameObject DyingEffect;
    public string bulletTag = "Bullet";
    public string baseTag = "Base";

    private bool IsDead = false;

    public WayPoints Path;
    public float Speed = 10f;
    public Transform Target;
    private int WayPointIndex = 0;

    void Start()
    {
        Target = Path.Points[0];
        currentHealth = maxHealth;

    }


    void Update()
    {
        if (this.gameObject.CompareTag("GroundEnemy"))
        {
            Vector3 Dir = Target.position - transform.position;
            transform.Translate(Dir.normalized * Speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, Target.position) <= 0.3f)
            {
                GetNextWayPoint();
            }
        }

        if (this.gameObject.CompareTag("AirEnemy"))
        {
            Target = Path.Points[Path.Points.Length-2];
            Vector3 Dir = Target.position - transform.position;
            transform.Translate(Dir.normalized * Speed * Time.deltaTime, Space.World);
        }
        
       
    }

    public void EnemyDied()
    {
        IsDead = true;

        
        GameObject EffectINS = (GameObject)Instantiate(DyingEffect, transform.position, transform.rotation);
        Destroy(EffectINS, 2f);
        EnemySpawner.DecreaseEnemyCount();
        Destroy(gameObject);
        //Debug.Log(EnemySpawner.spawnedEnemyCount);
    }

    private void GetNextWayPoint()
    {
        if (WayPointIndex >= Path.Points.Length - 1)
        {
            EndPath();
            return;
        }

        WayPointIndex++;
        Target = Path.Points[WayPointIndex];
    }

    private void EndPath()
    {
        EnemySpawner.DecreaseEnemyCount();
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            EnemyDied();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == baseTag)
        {
            //Debug.Log("Collision");
            EnemyDied();
        }

        if (IsDead)
        {
            return;
        }
    }
}
