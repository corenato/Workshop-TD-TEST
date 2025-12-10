using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour
{
    [Header("Citation")]
    private string citation = "Written with the help of Leo Chevry.";

    public Canvas Canvas_Menu;
    public Canvas Canvas_Settings;

    public void StartGame()
    {
        SceneManager.LoadScene("S_Test_UI_InGame");
        Canvas_Settings.enabled = false;
    }

    public void SettingsScreen()
    {
        Canvas_Settings.enabled = true;
        Canvas_Menu.enabled = false;
    }

    public void ZooScreen()
    {
        SceneManager.LoadScene("S_Zoo");
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
