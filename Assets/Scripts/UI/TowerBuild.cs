using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class TowerBuild : MonoBehaviour
{
    [SerializeField] private GameObject buildPanel;
    [SerializeField] private GameObject halo;
    public ResourceManager resourceManager;

    public Vector3 offset;
    public TileManager tileManager;

    public GameObject turret;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tileManager = FindAnyObjectByType<TileManager>(); //Cherche le TileManager et l'assigne à la variable correspondante
        TileManager.instance.RegisterType(this); //Tout GameObject equipe de ce script s'identifie comme tuile constructible
    }


    public void SetHalo() //Illumine toutes les tiles sur lesquelles le joueur peut construire
    {
       
        if (turret != null) //Si une tourelle est déja construite sur une tile constructible alors rien ne se passe
        {
            return;
        }

        Instantiate(halo,transform.position + offset, Quaternion.identity);
    }

    public void SetHaloOff() //Eteint toutes les tiles sur lesquelles le joueur peut construire
    {
        GameObject[] HaloList = GameObject.FindGameObjectsWithTag("Halo");
        
        foreach (GameObject Halo in HaloList)
        {
            Destroy(Halo);
        }

    }



    public void InstallTurret() //Quand je clique sur une tile : 
    {
        if(turret != null) //Si une tourelle est déja construite sur une tile constructible alors rien ne se passe
        {
            return;
        }
        
        GameObject TurretToBuild = TileManager.instance.GetTurretToBuild();  //Detecte quelle tourelle est sélectionnee
        turret = (GameObject)Instantiate(TurretToBuild, transform.position, Quaternion.identity); //Construit la tourelle à l'emplacement de la tile
        turret.GetComponent<TurretBehaviorGround>().resourceManager=resourceManager;
        tileManager.DestroyHalo();

    }
}
