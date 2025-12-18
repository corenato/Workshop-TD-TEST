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

    public EnemySpawner enemySpawner;
    [Space]
    public GameObject mainBase;
    public GameObject DyingEffect;
    public string bulletTag = "Bullet";
    public string baseTag = "Base";
    public GameObject firstMesh;
    public GameObject secondMesh;
    public GameObject transfoVFX;
    public float transformationCountdown;

    private bool IsDead = false;

    public Vector3 targetPosition;
    public Vector3 offset;
    public WayPoints Path;
    public float Speed = 10f;
    public Transform Target;
    private int WayPointIndex = 0;
    private bool isDestroyed = false;
    private bool isTransformed = false;

    void Start()
    {
        currentHealth = maxHealth;
        mainBase = GameObject.FindGameObjectWithTag("Base");

        firstMesh.SetActive(true);
        secondMesh.SetActive(false);
        isTransformed = false;

        if (this.gameObject.CompareTag("GroundEnemy"))
        {
            Target = Path.Points[0];
            //maxHealth = 20;
            //Speed = 2f;
            //damageToBase = 4;
            //damageToTurret = 0;
            //scrapDrop = 5;
            transformationCountdown = 4f;
        }


        if (this.gameObject.CompareTag("AirEnemy"))
        {
            Target = mainBase.transform;
            Vector3 position = transform.position;
            position.y += offset.y;
            transform.position = position;
            maxHealth = 25;
            Speed = 2f;
            damageToBase = 4;
            damageToTurret = 0;
            scrapDrop = 6;
            transformationCountdown = 4f;
        }

        if (this.gameObject.CompareTag("KamikazeEnemy"))
        {
            Target = mainBase.transform;
            Vector3 position = transform.position;
            position.y += offset.y;
            transform.position = position;
            maxHealth = 12;
            Speed = 4f;
            damageToBase = 8;
            damageToTurret = 8;
            scrapDrop = 3;
        }

    }


    void Update()
    {
        if (this.gameObject.CompareTag("GroundEnemy"))
        {
           transformationCountdown -= Time.deltaTime;
            SetGroundPath();
            MeshTransformTimer();
        }

        if (this.gameObject.CompareTag("AirEnemy"))
        {
            transformationCountdown -= Time.deltaTime;
            SetAirPath();
            MeshTransformTimer();
        }

        if (this.gameObject.CompareTag("KamikazeEnemy"))
        {
            SetAirPath();
        }
    }

    public void EnemyDied()
    {
        //IsDead = true;
        Debug.Log("Enemy Died");

        //GameObject EffectINS = (GameObject)Instantiate(DyingEffect, transform.position, transform.rotation);
        //Destroy(EffectINS, 2f);
        enemySpawner.DecreaseEnemyCount(this.gameObject);
        Destroy(this.gameObject);
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
        enemySpawner.DecreaseEnemyCount(this.gameObject);
        Destroy(this.gameObject);
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
        if (this.gameObject.CompareTag("KamikazeEnemy") && other.gameObject.CompareTag("GroundTurret") && isTransformed == true)
        {
            if (other.gameObject.TryGetComponent(out TurretBehaviorGround groundTurret))
            {
                isDestroyed = true;
                other.gameObject.GetComponent<TurretBehaviorGround>().TakeDamage(damageToTurret);
                EnemyDied();
            }
        }

        if (this.gameObject.CompareTag("KamikazeEnemy") && other.gameObject.CompareTag("AirTurret") && isTransformed == true)
        {
            if (other.gameObject.TryGetComponent(out TurretBehaviorAir airTurret))
            {
                //Debug.Log("Collision");
                isDestroyed = true;
                other.gameObject.GetComponent<TurretBehaviorAir>().TakeDamage(damageToTurret);
                EnemyDied();
            }
        }


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
                
                if(isTransformed == false)
                {
                    MeshTransformTrigger();
                    isTransformed = true;
                }

            }

            if (this.gameObject.CompareTag("KamikazeEnemy") && other.gameObject.CompareTag("GroundTurret"))
            {
                //Debug.Log("Target changed : " + other.gameObject.name);
                Target = other.gameObject.transform;

                if (isTransformed == false)
                {
                    MeshTransformTrigger();
                    isTransformed = true;
                }
            }
            return;
        }

        if (IsDead)
        {
            return;
        }
    }

    //private void OnCollisionEnter(Collision other)
    //{
    //    Debug.Log("00");
    //    if (this.gameObject.CompareTag("KamikazeEnemy") && other.gameObject.CompareTag("GroundTurret") && isDestroyed == false)
    //    {
    //        Debug.Log("aa");
    //        if (other.gameObject.TryGetComponent(out TurretBehaviorGround groundTurret))
    //        {
    //            Debug.Log("bb");
    //            isDestroyed = true;
    //            other.gameObject.GetComponent<TurretBehaviorGround>().TakeDamage(damageToTurret);
    //            EnemyDied();
    //        }
    //    }

    //    if (this.gameObject.CompareTag("KamikazeEnemy") && other.gameObject.CompareTag("AirTurret") && isDestroyed == false)
    //    {
    //        if (other.gameObject.TryGetComponent(out TurretBehaviorAir airTurret))
    //        {
    //            //Debug.Log("Collision");
    //            isDestroyed = true;
    //            other.gameObject.GetComponent<TurretBehaviorAir>().TakeDamage(damageToTurret);
    //            EnemyDied();
    //        }
    //    }
    //}


    private void SetGroundPath()
    {
        Vector3 Dir = Target.position - transform.position;
        Dir.y = 0f;

        if(Dir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Dir);
            transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    5f * Time.deltaTime
                );
        }

        transform.Translate(Dir.normalized * Speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, Target.position) <= 0.7f)
        {
            GetNextWayPoint();
        }
    }

    private void SetAirPath()
    {
        targetPosition = Target.transform.position;
        targetPosition.y += offset.y;
        Vector3 Dir = Target.position - transform.position;
        
        if (Dir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Dir);
            transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    5f * Time.deltaTime
                );
        }

        transform.Translate(Dir.normalized * Speed * Time.deltaTime, Space.World);
    }

    private void MeshTransformTimer()
    {
        if (transformationCountdown <= 0 && isTransformed == false)
        {
            Instantiate(transfoVFX,transform.position, Quaternion.identity, transform);
            Invoke("SetNewMesh", 0.5f);
            isTransformed = true;
        }
    }

    private void MeshTransformTrigger()
    {
        firstMesh.SetActive(false);
        Instantiate(transfoVFX, transform.position, Quaternion.identity, transform);
        secondMesh.SetActive(true);
        isTransformed = true;
    }

    private void SetNewMesh()
    {
        firstMesh.SetActive(false);
        secondMesh.SetActive(true);
    }
}
