using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Citation")]
    private string citation = "Written with the help of Leo Chevry.";


    
    public Canvas PauseCanvas;
    public bool isActive = false;


        public void Start()
    {
        PauseCanvas.enabled = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isActive)
            {
                PauseCanvas.enabled = true;
                Time.timeScale = 0f;
            }
            else
            {
                Resume();
            }
        }

        //Debug
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isActive)
            {
                PauseCanvas.enabled = true;
                Time.timeScale = 0f;
            }
            else
            {
                Resume();
            }
        }
    }


    public void Resume()
    {
        PauseCanvas.enabled = false;
        isActive = false;
        Time.timeScale = 1f;
    }

    public void SettingsScreen()
    {
        SceneManager.LoadScene("S_Settings");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("S_MainMenu");
    }

}
