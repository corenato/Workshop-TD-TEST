using UnityEngine;
using UnityEngine.UI;

public class TurretBehaviorAir : MonoBehaviour
{
   
    [SerializeField] private Transform Target;
    [SerializeField] public int turretMaxHealth;
    [SerializeField] public int turretCurrentHealth;
    [SerializeField] private float TurnSpeed = 10f;
    public ResourceManager resourceManager;
    public EnemySpawner enemySpawner;

    public float Range = 2f;
    public string airEnemyTag = "AirEnemy";
    public Transform PartToRotate;
    public float Firerate = 1f;
    public float FireCoutDown = 0f;
    public int turretBulletDamage;
    public bool isLevel2Damage;
    public bool isLevel2Range;
    public bool canShoot;

    public Vector3 rotationOffset;
    public GameObject smokeVFX;
    public GameObject towerPanel;
    public GameObject upgradePanel;
    public GameObject BulletPrefab;
    public Transform FirePoint;
    public Button damageLv2Button;
    public Button rangeLv2Button;
    public Button damageLV3Type1Button;
    public Button damageLV3Type2Button;
    public Button rangeLV3Type1Button;
    public Button rangeLV3Type2Button;

    public string[] targetTags = new[] { "AirEnemy","KamikazeEnemy" };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upgradePanel.SetActive(false);
        damageLv2Button.interactable = true;
        rangeLv2Button.interactable = true;
        damageLV3Type1Button.interactable = false;
        damageLV3Type2Button.interactable = false;
        rangeLV3Type1Button.interactable = false;
        rangeLV3Type2Button.interactable = false;
        canShoot = true;
        InvokeRepeating("UpdateTarget", 0f, 0.25f);
        //turretBulletDamage = 5;
        //Firerate = 1.43f;
        //Range = 4f;
        //turretMaxHealth = 5;
        turretCurrentHealth = turretMaxHealth;
        resourceManager = ResourceManager.Instance;
        enemySpawner = FindAnyObjectByType<EnemySpawner>();
        Debug.Log(smokeVFX.activeSelf);

        if (isLevel2Damage)
        {
            damageLv2Button.interactable = false;
            rangeLv2Button.interactable = false;
            damageLV3Type1Button.interactable = true;
            damageLV3Type2Button.interactable = true;
            rangeLV3Type1Button.interactable = false;
            rangeLV3Type2Button.interactable = false;
        }

        if (isLevel2Range)
        {
            damageLv2Button.interactable = false;
            rangeLv2Button.interactable = false;
            damageLV3Type1Button.interactable = false;
            damageLV3Type2Button.interactable = false;
            rangeLV3Type1Button.interactable = true;
            rangeLV3Type2Button.interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemySpawner.isBuildPhase == true)
        {
            canShoot = true;
        }

        if (canShoot == true)
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
        //GameObject[] airEnemies = GameObject.FindGameObjectsWithTag(airEnemyTag); 

        float ShortestDistance = Mathf.Infinity;
        GameObject NearestEnemy = null;

        foreach (var tag in targetTags)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject enemy in enemies)
            {
                float DistanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (DistanceToEnemy < ShortestDistance)
                {
                    ShortestDistance = DistanceToEnemy;
                    NearestEnemy = enemy;
                }
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
            //smokeVFX.SetActive(true);
            canShoot = false;

        }
    }

    public void AirLV2DamageStats()
    {
        turretBulletDamage = 9;
        Range = 5f;
        turretMaxHealth = 10;
        turretCurrentHealth = turretMaxHealth;
        upgradePanel.SetActive(false);
        damageLv2Button.interactable = false;
        rangeLv2Button.interactable = false;
        damageLV3Type1Button.interactable = true;
        damageLV3Type2Button.interactable = true;
    }

    public void AirLV3Damage1Stats()
    {
        turretBulletDamage = 13;
        Firerate = 0.56f;
        Range = 6f;
        turretMaxHealth = 15;
        turretCurrentHealth = turretMaxHealth;
        upgradePanel.SetActive(false);
        damageLV3Type1Button.interactable = false;
        damageLV3Type2Button.interactable = false;
    }

    public void AirLV3Damage2Stats()
    {
        turretBulletDamage = 16;
        Firerate = 0.71f;
        Range = 6f;
        turretMaxHealth = 15;
        turretCurrentHealth = turretMaxHealth;
        upgradePanel.SetActive(false);
        damageLV3Type1Button.interactable = false;
        damageLV3Type2Button.interactable = false;
    }

    public void AirLV2RangeStats()
    {
        turretBulletDamage = 7;
        Range = 6f;
        turretMaxHealth = 10;
        turretCurrentHealth = turretMaxHealth;
        upgradePanel.SetActive(false);
        damageLv2Button.interactable = false;
        rangeLv2Button.interactable = false;
        rangeLV3Type1Button.interactable = true;
        rangeLV3Type2Button.interactable = true;
    }

    public void AirLV3Range1Stats()
    {
        turretBulletDamage = 10;
        Firerate = 0.56f;
        Range = 7f;
        turretMaxHealth = 15;
        turretCurrentHealth = turretMaxHealth;
        upgradePanel.SetActive(false);
        rangeLV3Type1Button.interactable = false;
        rangeLV3Type2Button.interactable = false;
    }

    public void AirLV3Range2Stats()
    {
        turretBulletDamage = 8;
        Firerate = 0.56f;
        Range = 9f;
        turretMaxHealth = 15;
        turretCurrentHealth = turretMaxHealth;
        upgradePanel.SetActive(false);
        rangeLV3Type1Button.interactable = false;
        rangeLV3Type2Button.interactable = false;
    }

    public void OnUpgradeButtonClick()
    {
        resourceManager = ResourceManager.Instance;
        resourceManager.selectedTurret = this.gameObject;
        resourceManager.turretBehaviorAir = this;
        towerPanel.SetActive(false);
        upgradePanel.SetActive(true);
    }

    public void CloseUpgradePanel()
    {
        upgradePanel.SetActive(false);
    }

}
