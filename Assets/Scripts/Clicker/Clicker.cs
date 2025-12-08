using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Clicker : MonoBehaviour
{
    [SerializeField] private float depthDetection = 1000f;
    [SerializeField] private MineBuild mineBuild;
    [SerializeField] private TowerBuild towerBuild;
    [SerializeField] private TileManager tileManager;

    private bool canBuild;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * depthDetection, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, depthDetection))
        {
            if (hit.collider.gameObject.CompareTag("Constructible"))
            {
                mineBuild = hit.collider.gameObject.GetComponent<MineBuild>();
                towerBuild = hit.collider.gameObject.GetComponent<TowerBuild>();
                canBuild = true;
            }

            else
            {
                mineBuild = null;
                towerBuild = null;
                canBuild = false;
            }
        }
    }

    public void OnBuild(InputAction.CallbackContext context)
    {
        if (canBuild)
        {
            if(mineBuild != null)
            {
                mineBuild.InstallMine();
            }
            
            if(towerBuild != null)
            {
                towerBuild.InstallTurret();
            }
        }
    }
}
