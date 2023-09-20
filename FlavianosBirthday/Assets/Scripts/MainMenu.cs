using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private PlayerInfo playerInfo;
    [SerializeField]
    LevelLoader levelLoader;

    [SerializeField] GameObject menu;
    [SerializeField] GameObject options;
    [SerializeField] GameObject keys;
    [SerializeField] GameObject areYouSure;
    [SerializeField] GameObject tickFPSOn;
    [SerializeField] GameObject tickFPSOff;
    [SerializeField] GameObject tickJoystickOn;
    [SerializeField] GameObject tickJoystickOff;


    private void Start()
    {
        SetDefaultValues();
    }

    private void Update()
    {
        //fps
        if (PlayerPrefs.GetInt("FPS") == 1)
        {
            tickFPSOn.SetActive(true);
            tickFPSOff.SetActive(false);
        }
        else
        {
            tickFPSOff.SetActive(true);
            tickFPSOn.SetActive(false);
        }

        //joystick
        if (PlayerPrefs.GetInt("Joystick") == 1)
        {
            tickJoystickOn.SetActive(true);
            tickJoystickOff.SetActive(false);
        }
        else
        {
            tickJoystickOff.SetActive(true);
            tickJoystickOn.SetActive(false);
        }
    }

    //Main menu
    public void NewGame()
    {
        Application.targetFrameRate = 60;
        
        //playerInfo.haveKey = false;
        PlayerPrefs.SetInt("HaveKey", 0);
        
        //playerInfo.doorsOpened = 0;
        PlayerPrefs.SetInt("DoorsOpened", 0);

        PlayerPrefs.SetInt("CakeEat", 0);

        //playerInfo.isDuck = false;
        PlayerPrefs.SetInt("IsDuck", 0);

        PlayerPrefs.SetInt("HasFindingParadise", 0);
        PlayerPrefs.SetInt("HasGoingUnder", 0);
        PlayerPrefs.SetInt("HasBeforeYourEyes", 0);
        PlayerPrefs.SetInt("HasOri", 0);
        PlayerPrefs.SetInt("HasMyFriendPedro", 0);
        PlayerPrefs.SetInt("HasCultOfTheLamb", 0);

        PlayerPrefs.SetInt("HasFindingParadiseKey", 0);
        PlayerPrefs.SetInt("HasGoingUnderKey", 0);
        PlayerPrefs.SetInt("HasBeforeYourEyesKey", 0);
        PlayerPrefs.SetInt("HasOriKey", 0);
        PlayerPrefs.SetInt("HasMyFriendPedroKey", 0);
        PlayerPrefs.SetInt("HasCultOfTheLambKey", 0);

        PlayerPrefs.SetInt("NewGame", 1);
        PlayerPrefs.SetInt("GameFinished", 0);

        playerInfo.NewGame();
        playerInfo.isTalking = false;
        playerInfo.currentScene = "MainRoom";
        playerInfo.CloseAllDoors();
        playerInfo.playerPosition = new Vector3(0, 1.4f, 0);
        levelLoader.LoadLevel(playerInfo.currentScene);
        //Debug.Log(SceneManager.sceneCount);
    }

    public void Continue()
    {
        if (PlayerPrefs.GetInt("GameFinished") == 0 && PlayerPrefs.GetInt("NewGame") != 0)
        {
            //to the moon
            if (PlayerPrefs.GetInt("HasFindingParadise") == 1 && PlayerPrefs.GetInt("HasFindingParadiseKey") == 1 || PlayerPrefs.GetInt("HasFindingParadise") == 1 && PlayerPrefs.GetInt("HasFindingParadiseKey") == 0 || PlayerPrefs.GetInt("HasFindingParadise") == 0 && PlayerPrefs.GetInt("HasFindingParadiseKey") == 1)
            {
                playerInfo.talkedToWatts1 = true;
                playerInfo.talkedToWatts2 = true;
                playerInfo.talkedToWatts3 = true;
                playerInfo.talkedToWatts4 = true;
                playerInfo.enigmaSolved = true;
                playerInfo.HintTapeActive = true;
            }

            //going under
            if (PlayerPrefs.GetInt("HasGoingUnder") == 1 && PlayerPrefs.GetInt("HasGoingUnderKey") == 1 || PlayerPrefs.GetInt("HasGoingUnder") == 1 && PlayerPrefs.GetInt("HasGoingUnderKey") == 0 || PlayerPrefs.GetInt("HasGoingUnder") == 0 && PlayerPrefs.GetInt("HasGoingUnderKey") == 1)
            {
                playerInfo.talkedToStatistiko1 = true;
                playerInfo.statisticFinished = true;
            }

            //before your eyes
            if (PlayerPrefs.GetInt("HasBeforeYourEyes") == 1 && PlayerPrefs.GetInt("HasBeforeYourEyesKey") == 1 || PlayerPrefs.GetInt("HasBeforeYourEyes") == 1 && PlayerPrefs.GetInt("HasBeforeYourEyesKey") == 0 || PlayerPrefs.GetInt("HasBeforeYourEyes") == 0 && PlayerPrefs.GetInt("HasBeforeYourEyesKey") == 1)
            {
                playerInfo.mazeFinished = true;
            }

            //ori
            if (PlayerPrefs.GetInt("HasOri") == 1 && PlayerPrefs.GetInt("HasOriKey") == 1 || PlayerPrefs.GetInt("HasOri") == 1 && PlayerPrefs.GetInt("HasOriKey") == 0 || PlayerPrefs.GetInt("HasOri") == 0 && PlayerPrefs.GetInt("HasOriKey") == 1)
            {
                playerInfo.talkedToNaru1 = true;
                playerInfo.talkedToNaru2 = true;
                playerInfo.talkedToKu1 = true;
                playerInfo.chocoboFound = true;
                playerInfo.isabelFound = true;
                playerInfo.dogFound = true;
                playerInfo.strayFound = true;
            }

            //my friend Pedro
            if (PlayerPrefs.GetInt("HasMyFriendPedro") == 1 && PlayerPrefs.GetInt("HasMyFriendPedroKey") == 1 || PlayerPrefs.GetInt("HasMyFriendPedro") == 1 && PlayerPrefs.GetInt("HasMyFriendPedroKey") == 0 || PlayerPrefs.GetInt("HasMyFriendPedro") == 0 && PlayerPrefs.GetInt("HasMyFriendPedroKey") == 1)
            {
                playerInfo.bulletsStopped = true;
                playerInfo.leverPulled = true;
            }

            //cult of the lamb
            if (PlayerPrefs.GetInt("HasCultOfTheLamb") == 1 && PlayerPrefs.GetInt("HasCultOfTheLambKey") == 1 || PlayerPrefs.GetInt("HasCultOfTheLamb") == 1 && PlayerPrefs.GetInt("HasCultOfTheLambKey") == 0 || PlayerPrefs.GetInt("HasCultOfTheLamb") == 0 && PlayerPrefs.GetInt("HasCultOfTheLambKey") == 1)
            {
                playerInfo.hasGun = true;
                playerInfo.hasBranch = true;
                playerInfo.hasHardDisk = true;
                playerInfo.hasEyeball = true;
                playerInfo.hasPlatypus = true;
                playerInfo.placedGun = true;
                playerInfo.placedHardDisk = true;
                playerInfo.placedBranch = true;
                playerInfo.placedEyeball = true;
                playerInfo.placedPlatypus = true;
                playerInfo.sacrificedDone = true; 
            }

            playerInfo.isTalking = false;
            playerInfo.doorsOpened = PlayerPrefs.GetInt("DoorsOpened");
            playerInfo.currentScene = "MainRoom";
            playerInfo.playerPosition = new Vector3(0, 1.4f, 0);
            levelLoader.LoadLevel(playerInfo.currentScene);
        }
    }

    public void Options()
    {
        menu.SetActive(false);
        options.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
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

    public void Keys()
    {
        options.SetActive(false);
        keys.SetActive(true);
    }

    public void ResetGame()
    {
        options.SetActive(false);
        areYouSure.SetActive(true);
    }

    public void OptionsBack()
    {
        options.SetActive(false);
        menu.SetActive(true);
    }


    //keys
    public void KeysBack()
    {
        keys.SetActive(false);
        options.SetActive(true);
    }


    //Are you sure
    public void AYSYes()
    {
        PlayerPrefs.DeleteAll();
        SetDefaultValues();
        areYouSure.SetActive(false);
        options.SetActive(true);
    }

    public void AYSNo()
    {
        areYouSure.SetActive(false);
        options.SetActive(true);
    }


    //default values
    public void SetDefaultValues()
    {
        //default FPS
        if (!PlayerPrefs.HasKey("FPS")) PlayerPrefs.SetInt("FPS", 1);

        //default joystick
        if (!PlayerPrefs.HasKey("Joystick")) PlayerPrefs.SetInt("Joystick", 1);

        //default finished game
        if (!PlayerPrefs.HasKey("GameFinished")) PlayerPrefs.SetInt("GameFinished", 0);

        //default new game
        if (!PlayerPrefs.HasKey("NewGame")) PlayerPrefs.SetInt("NewGame", 0);
    }
}
