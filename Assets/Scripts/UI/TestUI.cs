using TMPro;
using UnityEngine;

public class TestUI : MonoBehaviour
{
    [SerializeField] private TestBase mainBase;
    public GameObject gameOverPanel;
    public GameObject winPanel;
    [SerializeField] private CopperMine copperMine;
    [SerializeField] private GoldMine goldMine;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private TextMeshProUGUI scrapAmount;
    [SerializeField] private TextMeshProUGUI copperAmount;
    [SerializeField] private TextMeshProUGUI goldAmount;
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI currentWave;
    [SerializeField] private TextMeshProUGUI remainingWaves;
    [SerializeField] private TextMeshProUGUI buildPhaseTimer;
    [SerializeField] private GameObject timerPanel;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        scrapAmount.text = mainBase.currentScrap.ToString();
        health.text = mainBase.currentBaseHealth.ToString();
        currentWave.text = enemySpawner.WaveIndex.ToString();
        remainingWaves.text = enemySpawner.remainingWaves.ToString();

        if (enemySpawner.remainingWaves == 0)
        {
            remainingWaves.text = "Final Wave ! ";
        }

        buildPhaseTimer.text = enemySpawner.fixedCountdown.ToString();

        if(enemySpawner.isBuildPhase == true)
        {
            timerPanel.SetActive(true);
        }

        else
        {
            timerPanel.SetActive(false);
        }
            
        copperAmount.text = enemySpawner.copperMine.resourceTotal.ToString();
        goldAmount.text = enemySpawner.goldMine.resourceTotal.ToString();
    }
}
