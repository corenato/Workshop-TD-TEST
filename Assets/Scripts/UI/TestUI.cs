using TMPro;
using UnityEngine;

public class TestUI : MonoBehaviour
{
    [SerializeField] private TestBase mainBase;
    [SerializeField] private CopperMine copperMine;
    [SerializeField] private GoldMine goldMine;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private TextMeshProUGUI scrapAmount;
    [SerializeField] private TextMeshProUGUI copperAmount;
    [SerializeField] private TextMeshProUGUI goldAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scrapAmount.text = "Scrap : " + mainBase.currentScrap;
        copperAmount.text = "Copper : " + enemySpawner.copperMine.resourceTotal;
        goldAmount.text = "Gold : " + enemySpawner.goldMine.resourceTotal;
    }
}
