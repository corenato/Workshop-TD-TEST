using UnityEngine;
using UnityEngine.UI;

public class GoldMine : MonoBehaviour
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

    public GameObject upgradePanel;
    public Button productionLV1Button;
    public Button productionLV2Button;
    public Button durabilityLV1Button;
    public Button durabilityLV2Button;
    public Button repairButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
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
        enemySpawner.goldMine = this;
        hasMinedThisTurn = false;
        upgradePanel.SetActive(false);
        productionLV1Button.interactable = true;
        productionLV2Button.interactable = false;
        durabilityLV1Button.interactable = true;
        durabilityLV2Button.interactable = false;
        repairButton.interactable = false;
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
                repairButton.interactable = true;
            }
        }
    }

    public void ProductionLV1()
    {
        mineProductionLevel++;
        mineGlobalLevel++;
        resourceRaw = Mathf.CeilToInt((float)(resourceRaw * 1.5));
        upgradePanel.SetActive(false);
        productionLV1Button.interactable = false;
        productionLV2Button.interactable = true;
    }

    public void ProductionLV2()
    {
        mineProductionLevel++;
        mineGlobalLevel++;
        resourceRaw = Mathf.CeilToInt((float)(resourceRaw * 1.5));
        upgradePanel.SetActive(false);
        productionLV2Button.interactable = false;
    }

    public void DurabilityLV1()
    {
        mineDurability++;
        mineDurabilityLevel++;
        mineGlobalLevel++;
        isMining = true;
        upgradePanel.SetActive(false);
        durabilityLV1Button.interactable = false;
        durabilityLV2Button.interactable = true;
    }

    public void DurabilityLV2()
    {
        mineDurability++;
        mineDurabilityLevel++;
        mineGlobalLevel++;
        isMining = true;
        upgradePanel.SetActive(false);
        durabilityLV2Button.interactable = false;
    }

    public void RepairMine()
    {
        mineDurability = 3 + mineDurabilityLevel;
        isMining = true;
        repairButton.interactable = false;
    }
}

