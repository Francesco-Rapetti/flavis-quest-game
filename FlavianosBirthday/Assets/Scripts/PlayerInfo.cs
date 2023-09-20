using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfo", menuName = "PlayerInfo")]
public class PlayerInfo : ScriptableObject
{
    //public bool isDuck = false;
    public float speed = 2.5f;
    public int doorsOpened = 0;
    //public bool haveKey = false;
    public string currentScene;

    /*[Header("Games")]
    public bool hasFindingParadise;
    public bool hasGoingUnder;
    public bool hasBeforeYourEyes;
    public bool hasOri;
    public bool hasMyFriendPedro;
    public bool hasCultOfTheLamb;

    [Header("Keys")]
    public bool hasFindingParadiseKey;
    public bool hasGoingUnderKey;
    public bool hasBeforeYourEyesKey;
    public bool hasOriKey;
    public bool hasMyFriendPedroKey;
    public bool hasCultOfTheLambKey;*/

    [Header("MainRoom")]
    /*public bool doorTTMOpen = false;
    public bool doorGUOpen = false;
    public bool doorOOpen = false;
    public bool doorBYEOpen = false;
    public bool doorMFPOpen = false; 
    public bool doorCOTLOpen = false;
    public bool cakeEat = false;*/
    public Vector3 playerPosition = new Vector3(0, 1.4f, 0);

    [Header("ToTheMoon")]
    public bool talkedToWatts1 = false;
    public bool talkedToWatts2 = false;
    public bool talkedToWatts3 = false;
    public bool talkedToWatts4 = false;
    public bool enigmaSolved = false;
    public bool HintTapeActive = false;
    public string orator = "Watts";

    [Header("Ori")]
    public bool talkedToNaru1 = false;
    public bool talkedToNaru2 = false;
    public bool talkedToKu1 = false;
    public bool chocoboFound = false;
    public bool isabelFound = false;
    public bool dogFound = false;
    public bool strayFound = false;

    [Header("GoingUnder")]
    public bool talkedToStatistiko1 = false;
    public bool statisticFinished = false;

    [Header("MyFriendPedro")]
    public bool bulletsStopped = false;
    public bool leverPulled = false;

    [Header("BeforeYourEyes")]
    public bool mazeFinished = false;

    [Header("CultOfTheLamb")]
    public bool hasGun = false;
    public bool hasHardDisk = false;
    public bool hasBranch = false;
    public bool hasEyeball = false;
    public bool hasPlatypus = false;
    public bool placedGun = false;
    public bool placedHardDisk = false;
    public bool placedBranch = false;
    public bool placedEyeball = false;
    public bool placedPlatypus = false;
    public bool sacrificedDone = false;

    

    [Header("States")]
    public bool isTalking = false;

    //dialogs functions
    public void SetTalkToWatts1() { talkedToWatts1 = true; orator = "Watts"; }
    public void SetTalkToWatts2() { talkedToWatts2 = true; orator = "Watts"; }
    public void SetTalkToWatts3() { talkedToWatts3 = true; orator = "Watts"; }
    public void SetTalkToWatts4() { talkedToWatts4 = true; orator = "Watts"; }
    public void SetOratorStaticBoy() { orator = "StaticBoy"; }
    public void SetOratorHintTape() { orator = "HintTape"; }
    public void SetOratorNaru() { orator = "Naru"; }
    public void SetOratorKu() { orator = "Ku"; }
    public void SetTalkToNaru1() { talkedToNaru1 = true; }
    public void SetTalkToNaru2() { talkedToNaru2 = true; }
    public void SetTalkToKu1() { talkedToKu1 = true; }
    public void SetOratorMirror() { orator = "Mirror"; }
    public void SetOratorStatistiko() { orator = "Statistiko"; }
    public void SetTalkToStatistiko1() { talkedToStatistiko1 = true; }
    public void SetStatisticFinished() { statisticFinished = true; }
    public void PullLever() { leverPulled = true; }
    public void SetOratorPascal() { orator = "Pascal"; }
    public void SetOratorPedro() { orator = "Pedro"; }
    public void SetOratorEasterEgg() { orator = "EasterEgg"; }
    public void SetOratorWerewolf() { orator = "Werewolf"; }
    public void SetMazeFInished() { mazeFinished = true; }
    public void SetOratorEdmund() { orator = "Edmund"; }
    public void SetHasGun() { hasGun = true; }
    public void SetHasHardDisk() { hasHardDisk = true; }
    public void SetHasBranch() { hasBranch = true; }
    public void SetHasEyeball() { hasEyeball = true; }
    public void SetHasPlatypus() { hasPlatypus = true; }
    public void SetPlacedGun() { placedGun = true; }
    public void SetPlacedHardDisk() { placedHardDisk = true; }
    public void SetPlacedBranch() {  placedBranch = true; }
    public void SetPlacedEyeball() { placedEyeball = true; }
    public void SetPlacedPlatypus() {  placedPlatypus = true; }
    public void SetSacrificeDone() { sacrificedDone = true; }

    //doors functions
    public void CloseAllDoors()
    {
        PlayerPrefs.SetInt("DoorTTMOpen", 0);
        PlayerPrefs.SetInt("DoorGUOpen", 0);
        PlayerPrefs.SetInt("DoorBYEOpen", 0);
        PlayerPrefs.SetInt("DoorOOpen", 0);
        PlayerPrefs.SetInt("DoorMFPOpen", 0);
        PlayerPrefs.SetInt("DoorCOTLOpen", 0);

        /*doorTTMOpen = false;
        doorGUOpen = false;
        doorOOpen = false;
        doorBYEOpen = false;
        doorMFPOpen = false;
        doorCOTLOpen = false;*/
    }

    public void OpenToTheMoonDoor()
    {
        PlayerPrefs.SetInt("DoorTTMOpen", 1);
        PlayerPrefs.SetInt("HaveKey", 0);
        PlayerPrefs.SetInt("DoorsOpened", ++doorsOpened);
    }

    public void OpenGoingUnderDoor()
    {
        PlayerPrefs.SetInt("DoorGUOpen", 1);
        PlayerPrefs.SetInt("HaveKey", 0);
        PlayerPrefs.SetInt("DoorsOpened", ++doorsOpened);
    }

    public void OpenOriDoor()
    {
        PlayerPrefs.SetInt("DoorOOpen", 1);
        PlayerPrefs.SetInt("HaveKey", 0);
        PlayerPrefs.SetInt("DoorsOpened", ++doorsOpened);
    }

    public void OpenBeforeYourEyes()
    {
        PlayerPrefs.SetInt("DoorBYEOpen", 1);
        PlayerPrefs.SetInt("HaveKey", 0);
        PlayerPrefs.SetInt("DoorsOpened", ++doorsOpened);
    }

    public void OpenMyFriendPedtroDoor()
    {
        PlayerPrefs.SetInt("DoorMFPOpen", 1);
        PlayerPrefs.SetInt("HaveKey", 0);
        PlayerPrefs.SetInt("DoorsOpened", ++doorsOpened);
    }

    public void OpenCultOfTheLambDoor()
    {
        PlayerPrefs.SetInt("DoorCOTLOpen", 1);
        PlayerPrefs.SetInt("HaveKey", 0);
        PlayerPrefs.SetInt("DoorsOpened", ++doorsOpened);
    }

    public bool AllDoorsClosed()
    {
        if (PlayerPrefs.GetInt("DoorTTMOpen") == 0 && PlayerPrefs.GetInt("DoorGUOpen") == 0 && PlayerPrefs.GetInt("DoorBYEOpen") == 0 && PlayerPrefs.GetInt("DoorOOpen") == 0 && PlayerPrefs.GetInt("DoorMFPOpen") == 0 && PlayerPrefs.GetInt("DoorCOTLOpen") == 0) return true;
        return false;
    }

    public void NewGame()
    {
        doorsOpened = 0;


        talkedToWatts1 = false;
        talkedToWatts2 = false;
        talkedToWatts3 = false;
        talkedToWatts4 = false;
        enigmaSolved = false;
        HintTapeActive = false;

        talkedToNaru1 = false;
        talkedToNaru2 = false;
        talkedToKu1 = false;
        chocoboFound = false;
        isabelFound = false;
        dogFound = false;
        strayFound = false;

        talkedToStatistiko1 = false;
        statisticFinished = false;

        bulletsStopped = false;
        leverPulled = false;

        mazeFinished = false;

        hasGun = false;
        hasHardDisk = false;
        hasBranch = false;
        hasEyeball = false;
        hasPlatypus = false;
        placedGun = false;
        placedHardDisk = false;
        placedBranch = false;
        placedEyeball = false;
        placedPlatypus = false;
        sacrificedDone = false;
    }
}
