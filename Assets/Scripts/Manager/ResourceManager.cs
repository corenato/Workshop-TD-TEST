using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public CopperMine copperMine;
    public TestBase mainBase;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainBase = this.gameObject.GetComponent<TestBase>();
    }

    // Update is called once per frame
    void Update()
    {
        copperMine = FindAnyObjectByType<CopperMine>();
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
}
