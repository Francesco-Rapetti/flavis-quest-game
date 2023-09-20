using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject options;
    [SerializeField] GameObject keys;
    [SerializeField] GameObject pauseButton;
    [SerializeField] PlayerInfo playerInfo;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (playerInfo.isTalking) pauseButton.SetActive(false);
        else pauseButton.SetActive(true);
    }


    //pause menu
    public void Resume()
    {
        pauseMenu.SetActive(false);
    }   
    
    public void Options()
    {
        options.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void Keys()
    {
        keys.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    //Options
    public void FPSOn()
    {
        PlayerPrefs.SetInt("FPS", 1);
    }

    public void FPSOff()
    {
        PlayerPrefs.SetInt("FPS", 0);
    }

    public void ShowJoystick()
    {
        PlayerPrefs.SetInt("Joystick", 1);
    }

    public void HideJoystick()
    {
        PlayerPrefs.SetInt("Joystick", 0);
    }

    public void DropdownMenu()
    {

    }

    public void OptionsBack()
    {
        pauseMenu.SetActive(true);
        options.SetActive(false);
    }


    //keys
    public void KeysBack()
    {
        pauseMenu.SetActive(true);
        keys.SetActive(false);
    }
}
