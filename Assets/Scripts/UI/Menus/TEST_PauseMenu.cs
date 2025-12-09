using UnityEngine;

public class TEST_PauseMenu : MonoBehaviour
{
    [Header("Citation")]
    private string citation = "Written with the help of Ewan.";


    public Canvas PauseCanvas;
    public bool isActive = false;

    public GameObject pauseMenu;
    private bool isPaused = false;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }


}
