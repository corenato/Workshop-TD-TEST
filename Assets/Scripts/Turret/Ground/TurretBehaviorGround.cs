using UnityEngine;

public class TurretBehaviorGround : MonoBehaviour
{
   
    [SerializeField] private Transform Target;
    [SerializeField] private float TurnSpeed = 10f;
    [SerializeField] private int turretMaxHealth;
    [SerializeField] private int turretCurrentHealth;
    public ResourceManager resourceManager;
    
    public float Range = 2f;
    public string EnemyTag = "GroundEnemy";
    public Transform PartToRotate;
    public float Firerate = 1f;
    public float FireCoutDown = 0f;
    public int turretBulletDamage;

    public GameObject towerPanel;
    public GameObject upgradePanel;
    public GameObject BulletPrefab;
    public Transform FirePoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upgradePanel.SetActive(false);
        turretCurrentHealth = turretMaxHealth;
        InvokeRepeating("UpdateTarget", 0f, 0.25f);
        turretBulletDamage = 6;
    }

    // Update is called once per frame
    void Update()
    {
        FireCoutDown -= Time.deltaTime;

        if (Target == null)
        {
            return;
        }

        Vector3 Dir = Target.position - transform.position;
        Quaternion LookRotation = Quaternion.LookRotation(Dir);
        Vector3 Rotation = Quaternion.Lerp(PartToRotate.rotation, LookRotation, Time.deltaTime * TurnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(Rotation.x, Rotation.y, 0f);

        if (FireCoutDown <= 0f)
        {
            Shoot();
            FireCoutDown = Firerate;
        }
    }

    void Shoot()
    {
        GameObject BulletGO = (GameObject)Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        GroundBullet Bullet = BulletGO.GetComponent<GroundBullet>();

        if (Bullet != null)
        {
            Bullet.bulletDamage = turretBulletDamage;
            Bullet.Seek(Target);
        }
    }

    void UpdateTarget()
    {
        GameObject[] groundEnemies = GameObject.FindGameObjectsWithTag(EnemyTag);

        float ShortestDistance = Mathf.Infinity;
        GameObject NearestEnemy = null;

        foreach (GameObject enemy in groundEnemies)
        {
            float DistanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (DistanceToEnemy < ShortestDistance)
            {
                ShortestDistance = DistanceToEnemy;
                NearestEnemy = enemy;
            }

        }


        if (NearestEnemy != null && ShortestDistance <= Range)
        {
            Target = NearestEnemy.transform;
        }
        else
        {
            Target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }

    public void TakeDamage(int damage)
    {
        turretCurrentHealth -= damage;
        //Debug.Log("Damage");

        if (turretCurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void GroundLV2DamageStats() 
    {
        turretBulletDamage = 9;
        Range = 5f;
        turretMaxHealth = 10;
        turretCurrentHealth = turretMaxHealth;
        upgradePanel.SetActive(false);
    }

    public void GroundLV2FirerateStats()
    {
        Firerate = 1.1f;
        Range = 5f;
        turretMaxHealth = 10;
        turretCurrentHealth = turretMaxHealth;
        upgradePanel.SetActive(false);
    }

    public void OnUpgradeButtonClick()
    {
        towerPanel.SetActive(false);
        upgradePanel.SetActive(true);
        resourceManager.selectedTurret = this.gameObject;
    }
   
}
