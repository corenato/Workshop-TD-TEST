using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Canvas Canvas_Settings;
    public Canvas Canvas_GENERAL;
    public Canvas Canvas_CONTROLS;
    public Canvas PauseCanvas;
    public Canvas MainMenu;

    public Image I_Keyboard;
    public Image I_Gamepad;

    public bool SceneIsOpened = false;


    public void SetVolume(float volume)
    {
        //Debug.Log(volume);
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void Start()
    {
        Canvas_GENERAL.enabled = true;
        Canvas_CONTROLS.enabled = false;
        I_Keyboard.enabled = true;
        I_Gamepad.enabled = false;
    }

    [Header("Citation")]
    private string citation = "Written with the help of Evana Ferreti.";

    public void EscapeButtonClicked()
    {
        Canvas_Settings.enabled = false;

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("S_MainMenu"))
        {
            //Debug.Log("La scène 'Main Menu' est actuellement ouverte !");
            MainMenu.enabled = true;
        }
        else
        {
            //Debug.Log("Autre scène que le main menu.");
            PauseCanvas.enabled = true;
            Time.timeScale = 0f;
        }
    }


    public void GeneralButtonClicked()
    {
        Canvas_GENERAL.enabled = true;
        Canvas_CONTROLS.enabled = false;
    }

    public void ControlsButtonClicked()
    {
        Canvas_GENERAL.enabled = false;
        Canvas_CONTROLS.enabled = true;
    }

    public void KeyboardButtonClicked()
    {
        I_Keyboard.enabled = true;
        I_Gamepad.enabled = false;
    }

    public void GamepadButtonClicked()
    {
        I_Keyboard.enabled = false;
        I_Gamepad.enabled = true;
    }

}
