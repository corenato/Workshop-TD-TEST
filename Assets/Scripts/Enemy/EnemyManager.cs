using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    
    
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currentHealth;
    [SerializeField] private int damageToTurret;
    [SerializeField] private int damageToBase;
    [SerializeField] private int scrapDrop;
    
    public GameObject mainBase;
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
    private bool isDestroyed = false;

    void Start()
    {
        currentHealth = maxHealth;
        mainBase = GameObject.FindGameObjectWithTag("Base");

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
           SetGroundPath();
        }

        if (this.gameObject.CompareTag("AirEnemy"))
        {
           SetAirPath();
        }

        if (this.gameObject.CompareTag("KamikazeEnemy"))
        {
            SetAirPath();
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
            mainBase.GetComponent<TestBase>().ScrapGain(scrapDrop);
            EnemyDied();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Base"))
        {
            //Debug.Log("Collision");
            other.gameObject.GetComponent<TestBase>().TakeDamage(damageToBase);
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
            return;
        }

        if (IsDead)
        {
            return;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (this.gameObject.CompareTag("KamikazeEnemy") && other.gameObject.CompareTag("GroundTurret") && isDestroyed == false)
        {
            if (other.gameObject.TryGetComponent(out TurretBehaviorGround groundTurret))
            {
                //Debug.Log("Collision");
                isDestroyed = true;
                other.gameObject.GetComponent<TurretBehaviorGround>().TakeDamage(damageToTurret);
                EnemyDied();
            }
        }

        if (this.gameObject.CompareTag("KamikazeEnemy") && other.gameObject.CompareTag("AirTurret") && isDestroyed == false)
        {
            if (other.gameObject.TryGetComponent(out TurretBehaviorAir groundTurret))
            {
                //Debug.Log("Collision");
                isDestroyed = true;
                other.gameObject.GetComponent<TurretBehaviorAir>().TakeDamage(damageToTurret);
                EnemyDied();
            }
        }
    }


    private void SetGroundPath()
    {
        Vector3 Dir = Target.position - transform.position;
        transform.Translate(Dir.normalized * Speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, Target.position) <= 0.3f)
        {
            GetNextWayPoint();
        }
    }

    private void SetAirPath()
    {
        targetPosition = Target.transform.position;
        targetPosition.y += offset.y;
        Vector3 Dir = Target.position - transform.position;
        transform.Translate(Dir.normalized * Speed * Time.deltaTime, Space.World);
    }
}
