using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Clicker : MonoBehaviour
{
    [SerializeField] private float depthDetection = 1000f;
    [SerializeField] private LayerMask layers;
    public MineBuild mineBuild;
    [SerializeField] private TowerBuild towerBuild;
    [SerializeField] private ResourceManager resourceManager;
    [SerializeField] private TileManager tileManager;
    [SerializeField] private GameObject currentTurret;
    [SerializeField] private bool canUpgrade;


    private bool canBuild;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resourceManager = FindAnyObjectByType<ResourceManager>();
        tileManager = FindAnyObjectByType<TileManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * depthDetection, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, depthDetection, layers))
        {
            if (hit.collider.gameObject.CompareTag("Constructible"))
            {
                mineBuild = hit.collider.gameObject.GetComponent<MineBuild>();
                towerBuild = hit.collider.gameObject.GetComponent<TowerBuild>();
                canBuild = true;
                currentTurret = null;
                canUpgrade = false;
            }

            else if(hit.collider.gameObject.CompareTag("GroundTurret") || hit.collider.gameObject.CompareTag("AirTurret"))
            {
                //Debug.Log("Turret hovered");
                currentTurret = hit.collider.gameObject;
                canUpgrade = true;
                mineBuild = null;
                towerBuild = null;
                canBuild = false;
            }


            else
            {              
                mineBuild = null;
                towerBuild = null;
                canBuild = false;
                currentTurret = null;
                canUpgrade = false;
            }
        }
    }

    public void OnBuild(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (canBuild)
            {

                if (mineBuild != null && TileManager.instance.mineToBuild.CompareTag("CopperMine"))
                {
                    resourceManager.BuildCopperMine(mineBuild);
                }

                else if (mineBuild != null && TileManager.instance.mineToBuild.CompareTag("GoldMine"))
                {
                    resourceManager.BuildGoldMine(mineBuild);
                }

                else if (towerBuild != null && TileManager.instance.turretToBuild.CompareTag("GroundTurret"))
                {
                    resourceManager.BuildGroundTurret(towerBuild);
                    towerBuild.resourceManager = resourceManager;
                }

                else if (towerBuild != null && TileManager.instance.turretToBuild.CompareTag("AirTurret"))
                {
                    resourceManager.BuildGroundTurret(towerBuild);
                    towerBuild.resourceManager = resourceManager;
                }
            }

            if (canUpgrade)
            {
                if (currentTurret.CompareTag("GroundTurret"))
                {
                    currentTurret.GetComponent<TurretBehaviorGround>().towerPanel.SetActive(true);
                }

                if (currentTurret.CompareTag("AirTurret"))
                {
                    currentTurret.GetComponent<TurretBehaviorAir>().towerPanel.SetActive(true);
                }
            }
        }   
    }
}
