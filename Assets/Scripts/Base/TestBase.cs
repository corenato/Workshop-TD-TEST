using UnityEngine;

public class TestBase : MonoBehaviour
{
    [SerializeField] private int maxBaseHealth;
    [SerializeField] private int currentBaseHealth;

    [SerializeField] public int currentScrap;


    void Start()
    {
        currentBaseHealth = maxBaseHealth;
        currentScrap = 0;
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
        Debug.Log("Current scrap : " +  currentScrap);
    }
}
