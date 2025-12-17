using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [Header("Script réalisé avec l'aide de Lea Parnaudeau")]
    public CopperMine copperMine;
    public GoldMine goldMine;
    public TestBase mainBase;
    public ResourceManager resource;
    public Clicker clicker;
    public MineBuild[] mineBuild;
    public TowerBuild[] towerBuild;
    public GameObject selectedTurret;
    public TurretBehaviorGround turretBehaviorGround;
    public TurretBehaviorAir turretBehaviorAir;
    [SerializeField] private GameObject level2DamageTurretPrefab;
    [SerializeField] private GameObject level2FirerateTurretPrefab;
    [SerializeField] private GameObject level2DamageAirTurretPrefab;
    [SerializeField] private GameObject level2RangeAirTurretPrefab;
    [SerializeField] private GameObject levelUpVFX;

    public static ResourceManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainBase = this.gameObject.GetComponent<TestBase>();
        mineBuild = FindObjectsByType<MineBuild>((FindObjectsSortMode)FindObjectsInactive.Exclude);
        towerBuild = FindObjectsByType<TowerBuild>((FindObjectsSortMode) FindObjectsInactive.Exclude);
    }

    // Update is called once per frame
    void Update()
    {
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();
    }

    public void BuildCopperMine(MineBuild mineBuild)
    {
        if(mainBase.currentScrap >= 40)
        {
            mainBase.currentScrap -= 40;
            mineBuild.InstallMine();
        }
    }

    public void BuildGoldMine(MineBuild mineBuild)
    {
        if (mainBase.currentScrap >= 32 && copperMine.resourceTotal >= 4)
        {
            mainBase.currentScrap -= 32;
            copperMine.resourceTotal -= 4;
            mineBuild.InstallMine();
        }
    }

    public void UpgradeCopperProductionLV1()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();

        if (mainBase.currentScrap >= 60)
        {
            mainBase.currentScrap -= 60;
            copperMine.ProductionLV1();
        }
    }

    public void UpgradeCopperProductionLV2()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();

        if (mainBase.currentScrap >= 48 && copperMine.resourceTotal >= 16)
        {
            mainBase.currentScrap -= 48;
            copperMine.resourceTotal -= 16;
            copperMine.ProductionLV2();
        }
    }

    public void UpgradeCopperDurabilityLV1()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();

        if (mainBase.currentScrap >= 60)
        {
            mainBase.currentScrap -= 60;
            copperMine.DurabilityLV1();
        }
    }

    public void UpgradeCopperDurabilityLV2()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();

        if (mainBase.currentScrap >= 48 && copperMine.resourceTotal >= 16)
        {
            mainBase.currentScrap -= 48;
            copperMine.resourceTotal -= 16;
            copperMine.DurabilityLV2();
        }
    }

    public void RepairCopperMine()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();

        if (copperMine.isMining == false)
        {
            if (mainBase.currentScrap >= 50)
            {
                mainBase.currentScrap -= 50;
                copperMine.RepairMine();
            }
        }
    }

    public void UpgradeGoldProductionLV1()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();

        if (mainBase.currentScrap >= 42 && copperMine.resourceTotal >= 9)
        {
            mainBase.currentScrap -= 42;
            copperMine.resourceTotal -= 9;
            goldMine.ProductionLV1();
        }
    }

    public void UpgradeGoldProductionLV2()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();

        if (mainBase.currentScrap >= 27 && copperMine.resourceTotal >= 14 && goldMine.resourceTotal >= 7)
        {
            mainBase.currentScrap -= 27;
            copperMine.resourceTotal -= 14;
            goldMine.resourceTotal -= 7;
            goldMine.ProductionLV2();
        }
    }

    public void UpgradeGoldDurabilityLV1()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();

        if (mainBase.currentScrap >= 42 && copperMine.resourceTotal >= 9)
        {
            mainBase.currentScrap -= 42;
            copperMine.resourceTotal -= 9;
            goldMine.DurabilityLV1();
        }
    }

    public void UpgradeGoldDurabilityLV2()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();

        if (mainBase.currentScrap >= 27 && copperMine.resourceTotal >= 14 && goldMine.resourceTotal >= 7)
        {
            mainBase.currentScrap -= 27;
            copperMine.resourceTotal -= 14;
            goldMine.resourceTotal -= 7;
            goldMine.DurabilityLV2();
        }
    }

    public void RepairGoldMine()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();

        if (goldMine.isMining == false)
        {
            if (mainBase.currentScrap >= 50)
            {
                mainBase.currentScrap -= 50;
                goldMine.RepairMine();
            }
        }
    }

    public void BuildGroundTurret(TowerBuild towerBuild)
    {
        if(mainBase.currentScrap >= 30)
        {
            mainBase.currentScrap -= 30;
            towerBuild.InstallTurret();
        }
    }

    public void BuildAirTurret(TowerBuild towerBuild)
    {
        if(mainBase.currentScrap >= 40)
        {
            mainBase.currentScrap -= 40;
            towerBuild.InstallTurret();
        }
    }
    
    public void UpgradeGroundToLV2Damage()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        resource = FindAnyObjectByType<ResourceManager>();
        //turretBehaviorGround = selectedTurret.GetComponent<TurretBehaviorGround>();
        copperMine = FindAnyObjectByType<CopperMine>();
        
        if (mainBase.currentScrap >= 40 && copperMine.resourceTotal >= 10)
        {
            mainBase.currentScrap -= 40;
            copperMine.resourceTotal -= 10;
            Instantiate(levelUpVFX, resource.selectedTurret.transform.position, Quaternion.identity, transform);
            Instantiate(resource.level2DamageTurretPrefab, resource.selectedTurret.transform.position, resource.selectedTurret.transform.rotation);
            //resource.turretBehaviorGround.GroundLV2DamageStats();
            Destroy(resource.selectedTurret);
            resource.selectedTurret = null;
            resource.turretBehaviorGround = null;
        }
    }

    public void UpgradeGroundToLV3DamageType1()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        resource = FindAnyObjectByType<ResourceManager>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();
        //turretBehaviorGround = selectedTurret.GetComponent<TurretBehaviorGround>();

        if (mainBase.currentScrap >= 30 && copperMine.resourceTotal >= 15 && goldMine.resourceTotal >= 8)
        {
            mainBase.currentScrap -= 40;
            copperMine.resourceTotal -= 10;
            goldMine.resourceTotal -= 8;
            Instantiate(levelUpVFX, resource.selectedTurret.transform.position, Quaternion.identity, transform);
            resource.turretBehaviorGround.GroundLV3Damage1Stats();
            resource.selectedTurret = null;
            resource.turretBehaviorGround = null;
        }
    }

    public void UpgradeGroundToLV3DamageType2()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        resource = FindAnyObjectByType<ResourceManager>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();
        //turretBehaviorGround = selectedTurret.GetComponent<TurretBehaviorGround>();

        if (mainBase.currentScrap >= 30 && copperMine.resourceTotal >= 15 && goldMine.resourceTotal >= 8)
        {
            mainBase.currentScrap -= 30;
            copperMine.resourceTotal -= 15;
            goldMine.resourceTotal -= 8;
            Instantiate(levelUpVFX, resource.selectedTurret.transform.position, Quaternion.identity, transform);
            resource.turretBehaviorGround.GroundLV3Damage2Stats();
            resource.selectedTurret = null;
            resource.turretBehaviorGround = null;
        }
    }

    public void UpgradeGroundToLV2Firerate()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        resource = FindAnyObjectByType<ResourceManager>();
        copperMine = FindAnyObjectByType<CopperMine>();
        //turretBehaviorGround = selectedTurret.GetComponent<TurretBehaviorGround>();

        if (mainBase.currentScrap >= 40 && copperMine.resourceTotal >= 10)
        {
            mainBase.currentScrap -= 40;
            copperMine.resourceTotal -= 10;
            Instantiate(levelUpVFX, resource.selectedTurret.transform.position, Quaternion.identity, transform);
            Instantiate(resource.level2FirerateTurretPrefab, resource.selectedTurret.transform.position, resource.selectedTurret.transform.rotation);
            //resource.turretBehaviorGround.GroundLV2FirerateStats();
            Destroy(resource.selectedTurret);
            resource.selectedTurret = null;
            resource.turretBehaviorGround = null;
        }
    }

    public void UpgradeGroundToLV3FirerateType1()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        resource = FindAnyObjectByType<ResourceManager>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();
        //turretBehaviorGround = selectedTurret.GetComponent<TurretBehaviorGround>();

        if (mainBase.currentScrap >= 30 && copperMine.resourceTotal >= 15 && goldMine.resourceTotal >= 8)
        {
            mainBase.currentScrap -= 30;
            copperMine.resourceTotal -= 15;
            goldMine.resourceTotal -= 8;
            Instantiate(levelUpVFX, resource.selectedTurret.transform.position, Quaternion.identity, transform);
            resource.turretBehaviorGround.GroundLV3Firerate1Stats();
            resource.selectedTurret = null;
            resource.turretBehaviorGround = null;
        }
    }

    public void UpgradeGroundToLV3FirerateType2()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        resource = FindAnyObjectByType<ResourceManager>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();
        //turretBehaviorGround = selectedTurret.GetComponent<TurretBehaviorGround>();

        if (mainBase.currentScrap >= 30 && copperMine.resourceTotal >= 15 && goldMine.resourceTotal >= 8)
        {
            mainBase.currentScrap -= 30;
            copperMine.resourceTotal -= 15;
            goldMine.resourceTotal -= 8;
            Instantiate(levelUpVFX, resource.selectedTurret.transform.position, Quaternion.identity, transform);
            resource.turretBehaviorGround.GroundLV3Firerate2Stats();
            resource.selectedTurret = null;
            resource.turretBehaviorGround = null;
        }
    }

    public void UpgradeAirToLV2Damage()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        resource = FindAnyObjectByType<ResourceManager>();
        copperMine = FindAnyObjectByType<CopperMine>();
        //turretBehaviorAir = selectedTurret.GetComponent<TurretBehaviorAir>();

        if (mainBase.currentScrap >= 49 && copperMine.resourceTotal >= 11)
        {
            mainBase.currentScrap -= 49;
            copperMine.resourceTotal -= 11;
            //resource.turretBehaviorAir.AirLV2DamageStats();
            Instantiate(levelUpVFX, resource.selectedTurret.transform.position, Quaternion.identity, transform);
            Instantiate(resource.level2DamageAirTurretPrefab, resource.selectedTurret.transform.position, resource.selectedTurret.transform.rotation);
            Destroy(resource.selectedTurret);
            resource.selectedTurret = null;
            resource.turretBehaviorAir = null;
        }
    }

    public void UpgradeAirToLV3DamageType1()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        resource = FindAnyObjectByType<ResourceManager>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();
        //turretBehaviorAir = selectedTurret.GetComponent<TurretBehaviorAir>();

        if (mainBase.currentScrap >= 33 && copperMine.resourceTotal >= 17 && goldMine.resourceTotal >= 9)
        {
            mainBase.currentScrap -= 33;
            copperMine.resourceTotal -= 17;
            goldMine.resourceTotal -= 9;
            Instantiate(levelUpVFX, resource.selectedTurret.transform.position, Quaternion.identity, transform);
            resource.turretBehaviorAir.AirLV3Damage1Stats();
            resource.selectedTurret = null;
            resource.turretBehaviorAir = null;
        }
    }

    public void UpgradeAirToLV3DamageType2()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        resource = FindAnyObjectByType<ResourceManager>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();
        //turretBehaviorAir = selectedTurret.GetComponent<TurretBehaviorAir>();

        if (mainBase.currentScrap >= 33 && copperMine.resourceTotal >= 17 && goldMine.resourceTotal >= 9)
        {
            mainBase.currentScrap -= 33;
            copperMine.resourceTotal -= 17;
            goldMine.resourceTotal -= 9;
            Instantiate(levelUpVFX, resource.selectedTurret.transform.position, Quaternion.identity, transform);
            resource.turretBehaviorAir.AirLV3Damage2Stats();
            resource.selectedTurret = null;
            resource.turretBehaviorAir = null;
        }
    }

    public void UpgradeAirToLV2Range()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        resource = FindAnyObjectByType<ResourceManager>();
        copperMine = FindAnyObjectByType<CopperMine>();
        //turretBehaviorAir = selectedTurret.GetComponent<TurretBehaviorAir>();

        if (mainBase.currentScrap >= 49 && copperMine.resourceTotal >= 11)
        {
            mainBase.currentScrap -= 49;
            copperMine.resourceTotal -= 11;
            //resource.turretBehaviorAir.AirLV2RangeStats();
            Instantiate(levelUpVFX, resource.selectedTurret.transform.position, Quaternion.identity, transform);
            Instantiate(resource.level2RangeAirTurretPrefab, resource.selectedTurret.transform.position, resource.selectedTurret.transform.rotation);
            Destroy(resource.selectedTurret);
            resource.selectedTurret = null;
            resource.turretBehaviorAir = null;
        }
    }

    public void UpgradeAirToLV3RangeType1()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        resource = FindAnyObjectByType<ResourceManager>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();
        //turretBehaviorAir = selectedTurret.GetComponent<TurretBehaviorAir>();

        if (mainBase.currentScrap >= 33 && copperMine.resourceTotal >= 17 && goldMine.resourceTotal >= 9)
        {
            mainBase.currentScrap -= 33;
            copperMine.resourceTotal -= 17;
            goldMine.resourceTotal -= 9;
            Instantiate(levelUpVFX, resource.selectedTurret.transform.position, Quaternion.identity, transform);
            resource.turretBehaviorAir.AirLV3Range1Stats();
            resource.selectedTurret = null;
            resource.turretBehaviorAir = null;
        }
    }

    public void UpgradeAirToLV3RangeType2()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        resource = FindAnyObjectByType<ResourceManager>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();
        //turretBehaviorAir = selectedTurret.GetComponent<TurretBehaviorAir>();

        if (mainBase.currentScrap >= 33 && copperMine.resourceTotal >= 17 && goldMine.resourceTotal >= 9)
        {
            mainBase.currentScrap -= 33;
            copperMine.resourceTotal -= 17;
            goldMine.resourceTotal -= 9;
            Instantiate(levelUpVFX, resource.selectedTurret.transform.position, Quaternion.identity, transform);
            resource.turretBehaviorAir.AirLV3Range2Stats();
            resource.selectedTurret = null;
            resource.turretBehaviorAir = null;
        }
    }
}
