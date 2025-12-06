using UnityEngine;

public class CopperMine : MonoBehaviour
{
    [SerializeField] private int mineGlobalLevel;
    [SerializeField] private int mineProductionLevel;
    [SerializeField] private int mineDurabilityLevel;
    [SerializeField] private int mineDurability;
    [SerializeField] private int resourceGain;
    [SerializeField] private int resourceRaw;
    [SerializeField] public int resourceTotal;

    public TestBase mainBase;
    public EnemySpawner enemySpawner;

    public bool isMining = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mineGlobalLevel = 1;
        mineProductionLevel = 0;
        mineDurabilityLevel = 0;
        mineDurability = 3;
        resourceRaw = 2;
        resourceTotal = 0;
        isMining = true;
        enemySpawner.copperMine = this;
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
            Debug.Log("Before : " + resourceTotal);
            resourceTotal += resourceGain;
            UpdateDurability();
            Debug.Log("After : " + resourceTotal);
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

    public void UpgradeProduction()
    {
        mineProductionLevel++;
        resourceRaw = Mathf.CeilToInt((float)(resourceRaw * 1.5));
    }

    public void UpgradeDurability()
    {

    }
}
