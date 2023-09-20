using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    PlayerInfo playerInfo;

    [SerializeField]
    GameObject FPS;

    [SerializeField]
    GameObject Joystick;

    [SerializeField]
    GameObject JoystickBG;

    [SerializeField]
    GameObject tickFPSOn;

    [SerializeField]
    GameObject tickFPSOff;

    [SerializeField]
    GameObject tickJoystickOn;

    [SerializeField]
    GameObject tickJoystickOff;


    private void Awake()
    {
        Application.targetFrameRate = 60; 
        QualitySettings.vSyncCount = 0;
        Debug.Log("GameController awaken!");
    }

    

    private void Update()
    {
        //Application.targetFrameRate = 60;

        //fps
        if (PlayerPrefs.GetInt("FPS") == 1)
        {
            FPS.SetActive(true);
            tickFPSOn.SetActive(true);
            tickFPSOff.SetActive(false);
        }
        else
        {
            FPS.SetActive(false);
            tickFPSOff.SetActive(true);
            tickFPSOn.SetActive(false);
        }

        //joystick
        if (PlayerPrefs.GetInt("Joystick") == 1)
        {
            Joystick.SetActive(true);
            JoystickBG.SetActive(true);
            tickJoystickOn.SetActive(true);
            tickJoystickOff.SetActive(false);
        }
        else
        {
            Joystick.SetActive(false);
            JoystickBG.SetActive(false);
            tickJoystickOff.SetActive(true);
            tickJoystickOn.SetActive(false);
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        //SceneManager.LoadScene(playerInfo.currentScene);
        Application.targetFrameRate = 60;
    }


}
