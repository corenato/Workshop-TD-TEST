using UnityEngine;

public class TestBase : MonoBehaviour
{
    [SerializeField] private int maxBaseHealth;
    [SerializeField] private int currentBaseHealth;

    [SerializeField] public int currentScrap;
    [SerializeField] public int currentCopper;

    //[SerializeField] private CopperMine copperMine;
    //[SerializeField] private GameObject cpMine;


    void Start()
    {
        //cpMine = GameObject.FindGameObjectWithTag("CopperMine");
        //copperMine = cpMine.GetComponent<CopperMine>();
        currentBaseHealth = maxBaseHealth;
        currentScrap = 0;
        //currentCopper = copperMine.resourceTotal;
    }

    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        currentBaseHealth -= damage;
        //Debug.Log(currentBaseHealth);

        if (currentBaseHealth <= 0)
        {
            //GameOver();
        }
    }

    public void ScrapGain(int scrap)
    {
        currentScrap += scrap;
        //Debug.Log("Current scrap : " +  currentScrap);
    }
}
