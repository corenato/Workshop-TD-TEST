using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class TowerBuild : MonoBehaviour
{
    [SerializeField] private GameObject buildPanel;
    [SerializeField] private GameObject halo;
    
    
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //halo.SetActive(true);
        //Debug.Log("Halo avant : " + halo.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetHaloColor()
    {
        Debug.Log("hello");
        Debug.Log("Halo avant : " + halo.activeSelf);
        //halo.SetActive(false);
        Instantiate(halo,transform.position, Quaternion.identity);
        Debug.Log("Halo après : " + halo.activeSelf);
        Debug.Log("coucou");
    }
}
