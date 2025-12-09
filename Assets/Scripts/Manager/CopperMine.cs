using UnityEngine;

public class CopperMine : MonoBehaviour
{
    public int mineGlobalLevel;
    public int mineProductionLevel;
    public int mineDurabilityLevel;
    public int mineDurability;
    public int resourceGain;
    public int resourceRaw;
    public int resourceTotal;

    public TestBase mainBase;
    public ResourceManager resourceManager;
    public EnemySpawner enemySpawner;

    public bool isMining = true;
    public bool hasMinedThisTurn;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //cpMine = GameObject.FindGameObjectWithTag("CopperMine");
        //resourceManager = mainBase.GetComponent<ResourceManager>();

        mineGlobalLevel = 1;
        mineProductionLevel = 0;
        mineDurabilityLevel = 0;
        mineDurability = 3;
        resourceRaw = 2;
        resourceTotal = 0;
        isMining = true;
        enemySpawner.copperMine = this;
        hasMinedThisTurn = false;
}

    // Update is called once per frame
    void Update()
    {
        resourceGain = resourceRaw * 5;
    }

    public void ProduceResource()
    {
        if (isMining == true)
        {
            if (hasMinedThisTurn == false)
            {
                resourceTotal += resourceGain;
                UpdateDurability();
                hasMinedThisTurn = true;
            }
        }
    }

    public void UpdateDurability()
    {
        if (isMining == true)
        {
            mineDurability--;

            if (mineDurability <= 0)
            {
                isMining = false;
            }
        }
    }

    public void UpgradeDurability()
    {
        mineDurability++;
        mineDurabilityLevel++;
        mineGlobalLevel++;
        isMining = true;
    }
}
