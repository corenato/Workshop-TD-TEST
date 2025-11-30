using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class TileManager : MonoBehaviour
{
    public static TileManager instance;
    public List <TowerBuild> buildableTiles = new List<TowerBuild>();
    public List <MineBuild> buildableMineTiles = new List<MineBuild> ();

    public GameObject turretToBuild;
    public GameObject turretAirPrefab;
    public GameObject turretGroundPrefab;

    public GameObject mineToBuild;
    public GameObject mineCopperPrefab;
    public GameObject mineGoldPrefab;




    private void Awake() //Permet d'appeler ce script dans d'autres scripts
    {
      instance = this;
    }

    public GameObject GetTurretToBuild() //Retourne quelle tourelle a ete selectionnee
    {
        return turretToBuild;
    }
    
    public void RegisterType(TowerBuild a) //Ajouter toutes les cases auto-identifiees comme constructibles dans la liste des tiles constructibles
    {
        buildableTiles.Add(a);
    }

    public void CreateHalo() //Creer le halo autour de chaque tile constructible qui fait partie de la liste
    {
        foreach (TowerBuild tile in TileManager.instance.buildableTiles)
        {
            if (turretToBuild == null)
            {
                tile.SetHalo();
            }
        }      

        
    }

    public void CreateHaloMine()
    {
        foreach (MineBuild tile in TileManager.instance.buildableMineTiles)
        {
            if (mineToBuild == null)
            {
                tile.SetHalo();
            }
        }
    }

    public void DestroyHalo()
    {
        foreach (TowerBuild tile in TileManager.instance.buildableTiles)
        {
            tile.SetHaloOff();
        }

        foreach (MineBuild tile in TileManager.instance.buildableMineTiles)
        {
            tile.SetHaloOff();
        }

        turretToBuild = null;
        mineToBuild = null;
    }

    public void SelectGroundTurret() //La tourelle sol est sélectionnee
    {
        turretToBuild = turretGroundPrefab;
    }

    public void SelectAirTurret() //La tourelle air est sélectionnee
    {
        turretToBuild = turretAirPrefab;
    }

    public GameObject GetMineToBuild() //Retourne quelle mine a ete selectionnee
    {
        return mineToBuild;
    }

    public void RegisterTypeMine(MineBuild a) //Ajouter toutes les cases auto-identifiees comme constructibles dans la liste des tiles constructibles
    {
        buildableMineTiles.Add(a);
    }

    public void SelectCopperMine()
    {
        mineToBuild = mineCopperPrefab;
    }

    public void SelectGoldMine()
    {
        mineToBuild = mineGoldPrefab;
    }
}
