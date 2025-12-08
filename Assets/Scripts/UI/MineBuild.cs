using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class MineBuild : MonoBehaviour /*IPointerDownHandler*/
{
    [SerializeField] private GameObject buildPanel;
    [SerializeField] private GameObject halo;

    public Vector3 offset;
    public TileManager tileManager;
    public TestBase mainBase;
    public EnemySpawner enemySpawner;

    public GameObject mine;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainBase = FindAnyObjectByType<TestBase>();
        enemySpawner = FindAnyObjectByType<EnemySpawner>();
        tileManager = FindAnyObjectByType<TileManager>(); //Cherche le TileManager et l'assigne à la variable correspondante
        TileManager.instance.RegisterTypeMine(this); //Tout GameObject equipe de ce script s'identifie comme tuile constructible
    }

    public void SetHalo() //Illumine toutes les tiles sur lesquelles le joueur peut construire
    {

        if (mine != null) //Si une mine est déja construite sur une tile constructible alors rien ne se passe
        {
            return;
        }

        Instantiate(halo, transform.position + offset, Quaternion.identity);
    }

    public void SetHaloOff() //Eteint toutes les tiles sur lesquelles le joueur peut construire
    {
        GameObject[] HaloList = GameObject.FindGameObjectsWithTag("Halo");

        foreach (GameObject Halo in HaloList)
        {
            Destroy(Halo);
        }

    }


    public void InstallMine() //Quand je clique sur une tile : 
    {
        if (mine != null) //Si une mine est déja construite sur une tile constructible alors rien ne se passe
        {
            return;
        }
        GameObject MineToBuild = TileManager.instance.GetMineToBuild();  //Detecte quelle mine est sélectionnee
        mine = (GameObject)Instantiate(MineToBuild, transform.position, Quaternion.identity); //Construit la mine à l'emplacement de la tile
        tileManager.DestroyHalo();
        mine.GetComponent<CopperMine>().mainBase = mainBase;
        mine.GetComponent<CopperMine>().enemySpawner = enemySpawner;
        mine.GetComponent<GoldMine>().mainBase = mainBase;
        mine.GetComponent<GoldMine>().enemySpawner = enemySpawner;
    }
}
