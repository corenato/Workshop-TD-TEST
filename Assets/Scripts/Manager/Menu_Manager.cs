using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour
{
    [Header("Citation")]
    private string citation = "Written with the help of Leo Chevry.";



    public void StartGame()
    {
        SceneManager.LoadScene("S_Blockout");
    }

    public void SettingsScreen()
    {
        SceneManager.LoadScene("S_Settings");
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
