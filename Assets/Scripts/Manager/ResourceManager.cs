using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public CopperMine copperMine;
    public GoldMine goldMine;
    public TestBase mainBase;
    public MineBuild[] mineBuild;
    public TowerBuild[] towerBuild;
    public GameObject selectedTurret;
    public TurretBehaviorGround turretBehaviorGround;
    public TurretBehaviorAir turretBehaviorAir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainBase = this.gameObject.GetComponent<TestBase>();
        mineBuild = FindObjectsByType<MineBuild>((FindObjectsSortMode)FindObjectsInactive.Include);
        towerBuild = FindObjectsByType<TowerBuild>((FindObjectsSortMode) FindObjectsInactive.Include);
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

    public void UpgradeCopperProduction()
    {
        if (copperMine.mineProductionLevel == 0)
        {
            if (mainBase.currentScrap >= 60)
            {
                mainBase.currentScrap -= 60;
                copperMine.mineProductionLevel++;
                copperMine.mineGlobalLevel++;
                copperMine.resourceRaw = Mathf.CeilToInt((float)(copperMine.resourceRaw * 1.5));
            }
        }

        else if (copperMine.mineProductionLevel == 1)
        {
            if (mainBase.currentScrap >= 48 && copperMine.resourceTotal >= 16)
            {
                mainBase.currentScrap -= 48;
                copperMine.resourceTotal -= 16;
                copperMine.mineProductionLevel++;
                copperMine.mineGlobalLevel++;
                copperMine.resourceRaw = Mathf.CeilToInt((float)(copperMine.resourceRaw * 1.5));
            }
        }
    }

    public void UpgradeCopperDurability()
    {
        if (copperMine.mineDurabilityLevel == 0)
        {
            if (mainBase.currentScrap >= 60)
            {
                mainBase.currentScrap -= 60;
                copperMine.mineDurabilityLevel++;
                copperMine.mineGlobalLevel++;
                copperMine.mineDurability ++;
                copperMine.isMining = true;
            }
        }
        else if (copperMine.mineDurabilityLevel == 1)
        {
            if (mainBase.currentScrap >= 48 && copperMine.resourceTotal >= 16)
            {
                mainBase.currentScrap -= 48;
                copperMine.resourceTotal -= 16;
                copperMine.mineDurabilityLevel++;
                copperMine.mineGlobalLevel++;
                copperMine.mineDurability ++;
                copperMine.isMining = true;
            }
        }
    }

    public void RepairCopperMine()
    {
        if (copperMine.mineDurability <= 0 && mainBase.currentScrap >= 50)
        {
            mainBase.currentScrap -= 50;
            copperMine.mineDurability = 3 + copperMine.mineDurabilityLevel;
            copperMine.isMining = true;
        }
    }

    public void UpgradeGoldProduction()
    {
        if (goldMine.mineProductionLevel == 0)
        {
            if (mainBase.currentScrap >= 42 && copperMine.resourceTotal >= 9)
            {
                mainBase.currentScrap -= 42;
                copperMine.resourceTotal -= 9;
                goldMine.mineProductionLevel++;
                goldMine.mineGlobalLevel++;
                goldMine.resourceRaw = Mathf.CeilToInt((float)(goldMine.resourceRaw * 1.5));
            }
        }

        else if (goldMine.mineProductionLevel == 1)
        {
            if (mainBase.currentScrap >= 27 && copperMine.resourceTotal >= 14 && goldMine.resourceTotal >= 7)
            {
                mainBase.currentScrap -= 27;
                copperMine.resourceTotal -= 14;
                goldMine.resourceTotal -= 7;
                goldMine.mineProductionLevel++;
                goldMine.mineGlobalLevel++;
                goldMine.resourceRaw = Mathf.CeilToInt((float)(goldMine.resourceRaw * 1.5));
            }
        }
    }

    public void UpgradeGoldDurability()
    {
        if (goldMine.mineDurabilityLevel == 0)
        {
            if (mainBase.currentScrap >= 42 && copperMine.resourceTotal >= 9)
            {
                mainBase.currentScrap -= 42;
                copperMine.resourceTotal -= 9;
                goldMine.mineDurabilityLevel++;
                goldMine.mineGlobalLevel++;
                goldMine.mineDurability++;
                goldMine.isMining = true;
            }
        }
        else if (goldMine.mineDurabilityLevel == 1)
        {
            if (mainBase.currentScrap >= 27 && goldMine.resourceTotal >= 14 && goldMine.resourceTotal >= 7)
            {
                mainBase.currentScrap -= 27;
                copperMine.resourceTotal -= 14;
                goldMine.resourceTotal -= 7;
                goldMine.mineDurabilityLevel++;
                goldMine.mineGlobalLevel++;
                goldMine.mineDurability++;
                goldMine.isMining = true;
            }
        }
    }

    public void RepairGoldMine()
    {
        if (goldMine.mineDurability <= 0 && mainBase.currentScrap >= 50)
        {
            mainBase.currentScrap -= 50;
            goldMine.mineDurability = 3 + goldMine.mineDurabilityLevel;
            goldMine.isMining = true;
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
        copperMine = FindAnyObjectByType<CopperMine>();
        turretBehaviorGround = selectedTurret.GetComponent<TurretBehaviorGround>();

        if(mainBase.currentScrap >= 40 && copperMine.resourceTotal >= 10)
        {
            mainBase.currentScrap -= 40;
            copperMine.resourceTotal -= 10;
            turretBehaviorGround.GroundLV2DamageStats();
            selectedTurret = null;
            turretBehaviorGround = null;
        }
    }

    public void UpgradeGroundToLV3DamageType1()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();
        turretBehaviorGround = selectedTurret.GetComponent<TurretBehaviorGround>();

        if (mainBase.currentScrap >= 30 && copperMine.resourceTotal >= 15 && goldMine.resourceTotal >= 8)
        {
            mainBase.currentScrap -= 40;
            copperMine.resourceTotal -= 10;
            goldMine.resourceTotal -= 8;
            turretBehaviorGround.GroundLV3Damage1Stats();
            selectedTurret = null;
            turretBehaviorGround = null;
        }
    }

    public void UpgradeGroundToLV3DamageType2()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();
        turretBehaviorGround = selectedTurret.GetComponent<TurretBehaviorGround>();

        if (mainBase.currentScrap >= 30 && copperMine.resourceTotal >= 15 && goldMine.resourceTotal >= 8)
        {
            mainBase.currentScrap -= 30;
            copperMine.resourceTotal -= 15;
            goldMine.resourceTotal -= 8;
            turretBehaviorGround.GroundLV3Damage2Stats();
            selectedTurret = null;
            turretBehaviorGround = null;
        }
    }

    public void UpgradeGroundToLV2Firerate()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        copperMine = FindAnyObjectByType<CopperMine>();
        turretBehaviorGround = selectedTurret.GetComponent<TurretBehaviorGround>();

        if (mainBase.currentScrap >= 40 && copperMine.resourceTotal >= 10)
        {
            mainBase.currentScrap -= 40;
            copperMine.resourceTotal -= 10;
            turretBehaviorGround.GroundLV2FirerateStats();
            selectedTurret = null;
            turretBehaviorGround = null;
        }
    }

    public void UpgradeGroundToLV3FirerateType1()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();
        turretBehaviorGround = selectedTurret.GetComponent<TurretBehaviorGround>();

        if (mainBase.currentScrap >= 30 && copperMine.resourceTotal >= 15 && goldMine.resourceTotal >= 8)
        {
            mainBase.currentScrap -= 30;
            copperMine.resourceTotal -= 15;
            goldMine.resourceTotal -= 8;
            turretBehaviorGround.GroundLV3Firerate1Stats();
            selectedTurret = null;
            turretBehaviorGround = null;
        }
    }

    public void UpgradeGroundToLV3FirerateType2()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();
        turretBehaviorGround = selectedTurret.GetComponent<TurretBehaviorGround>();

        if (mainBase.currentScrap >= 30 && copperMine.resourceTotal >= 15 && goldMine.resourceTotal >= 8)
        {
            mainBase.currentScrap -= 30;
            copperMine.resourceTotal -= 15;
            goldMine.resourceTotal -= 8;
            turretBehaviorGround.GroundLV3Firerate2Stats();
            selectedTurret = null;
            turretBehaviorGround = null;
        }
    }

    public void UpgradeAirToLV2Damage()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        copperMine = FindAnyObjectByType<CopperMine>();
        turretBehaviorAir = selectedTurret.GetComponent<TurretBehaviorAir>();

        if (mainBase.currentScrap >= 49 && copperMine.resourceTotal >= 11)
        {
            mainBase.currentScrap -= 49;
            copperMine.resourceTotal -= 11;
            turretBehaviorAir.AirLV2DamageStats();
            selectedTurret = null;
            turretBehaviorAir = null;
        }
    }

    public void UpgradeAirToLV3DamageType1()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();
        turretBehaviorAir = selectedTurret.GetComponent<TurretBehaviorAir>();

        if (mainBase.currentScrap >= 33 && copperMine.resourceTotal >= 17 && goldMine.resourceTotal >= 9)
        {
            mainBase.currentScrap -= 33;
            copperMine.resourceTotal -= 17;
            goldMine.resourceTotal -= 9;
            turretBehaviorAir.AirLV3Damage1Stats();
            selectedTurret = null;
            turretBehaviorAir = null;
        }
    }

    public void UpgradeAirToLV3DamageType2()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();
        turretBehaviorAir = selectedTurret.GetComponent<TurretBehaviorAir>();

        if (mainBase.currentScrap >= 33 && copperMine.resourceTotal >= 17 && goldMine.resourceTotal >= 9)
        {
            mainBase.currentScrap -= 33;
            copperMine.resourceTotal -= 17;
            goldMine.resourceTotal -= 9;
            turretBehaviorAir.AirLV3Damage2Stats();
            selectedTurret = null;
            turretBehaviorAir = null;
        }
    }

    public void UpgradeAirToLV2Range()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        copperMine = FindAnyObjectByType<CopperMine>();
        turretBehaviorAir = selectedTurret.GetComponent<TurretBehaviorAir>();

        if (mainBase.currentScrap >= 49 && copperMine.resourceTotal >= 11)
        {
            mainBase.currentScrap -= 49;
            copperMine.resourceTotal -= 11;
            turretBehaviorAir.AirLV2RangeStats();
            selectedTurret = null;
            turretBehaviorGround = null;
        }
    }

    public void UpgradeAirToLV3RangeType1()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();
        turretBehaviorAir = selectedTurret.GetComponent<TurretBehaviorAir>();

        if (mainBase.currentScrap >= 33 && copperMine.resourceTotal >= 17 && goldMine.resourceTotal >= 9)
        {
            mainBase.currentScrap -= 33;
            copperMine.resourceTotal -= 17;
            goldMine.resourceTotal -= 9;
            turretBehaviorAir.AirLV3Range1Stats();
            selectedTurret = null;
            turretBehaviorAir = null;
        }
    }

    public void UpgradeAirToLV3RangeType2()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        copperMine = FindAnyObjectByType<CopperMine>();
        goldMine = FindAnyObjectByType<GoldMine>();
        turretBehaviorAir = selectedTurret.GetComponent<TurretBehaviorAir>();

        if (mainBase.currentScrap >= 33 && copperMine.resourceTotal >= 17 && goldMine.resourceTotal >= 9)
        {
            mainBase.currentScrap -= 33;
            copperMine.resourceTotal -= 17;
            goldMine.resourceTotal -= 9;
            turretBehaviorAir.AirLV3Range2Stats();
            selectedTurret = null;
            turretBehaviorAir = null;
        }
    }
}
