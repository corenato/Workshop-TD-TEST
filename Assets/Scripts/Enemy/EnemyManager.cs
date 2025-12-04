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

    public Vector3 targetPosition;
    public Vector3 offset;
    public WayPoints Path;
    public float Speed = 10f;
    public Transform Target;
    private int WayPointIndex = 0;

    void Start()
    {
        currentHealth = maxHealth;

        if (this.gameObject.CompareTag("GroundEnemy"))
        {
            Target = Path.Points[0];
        }


        if (this.gameObject.CompareTag("AirEnemy"))
        {
            Target = mainBase.transform;
            Vector3 position = transform.position;
            // keep spawn height relative to original spawn Y; change to `p.y = offset.y;` for absolute world height
            position.y += offset.y;
            transform.position = position;
        }

        if (this.gameObject.CompareTag("KamikazeEnemy"))
        {
            Target = mainBase.transform;
            Vector3 position = transform.position;
            // keep spawn height relative to original spawn Y; change to `p.y = offset.y;` for absolute world height
            position.y += offset.y;
            transform.position = position;
        }

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

            targetPosition = Target.transform.position;
            targetPosition.y += offset.y;
            Vector3 Dir = Target.position - transform.position;
            transform.Translate(Dir.normalized * Speed * Time.deltaTime, Space.World);
        }

        if (this.gameObject.CompareTag("KamikazeEnemy"))
        {

            targetPosition = Target.transform.position;
            targetPosition.y += offset.y;
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
        if (other.gameObject.CompareTag("Base"))
        {
            Debug.Log("Collision");
            EnemyDied();
        }

        if (Target.gameObject.CompareTag("Base"))
        {
            if (this.gameObject.CompareTag("KamikazeEnemy") && other.gameObject.CompareTag("AirTurret"))
            {
                //Debug.Log("Target changed : " + other.gameObject.name);
                Target = other.gameObject.transform;
            }

            if (this.gameObject.CompareTag("KamikazeEnemy") && other.gameObject.CompareTag("GroundTurret"))
            {
                //Debug.Log("Target changed : " + other.gameObject.name);
                Target = other.gameObject.transform;
            }
        }

        

        if (IsDead)
        {
            return;
        }
    }
}
