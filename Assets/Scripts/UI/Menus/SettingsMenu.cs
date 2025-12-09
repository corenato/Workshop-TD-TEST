using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    
    public AudioMixer audioMixer;

    public Canvas Canvas_GENERAL;
    public Canvas Canvas_OPTIONS;

    public Image I_Keyboard;
    public Image I_Gamepad;

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
        Canvas_OPTIONS.enabled = false;
        I_Keyboard.enabled = true;
        I_Gamepad.enabled = false;
    }


    public void GeneralButtonClicked()
    {
        Canvas_GENERAL.enabled = true;
        Canvas_OPTIONS.enabled = false;
    }

    public void OptionsButtonClicked()
    {
        Canvas_GENERAL.enabled = false;
        Canvas_OPTIONS.enabled = true;
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
