using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    public PlayerInfo playerInfo;
    public GameObject doorToTheMoon;
    public GameObject doorGoingUnder;
    public GameObject doorOri;
    public GameObject doorBeforeYourEyes;
    public GameObject doorMyFriendPedro;
    public GameObject doorCultOfTheLamb;

    public bool operating = false;

    



    private void Update()
    {
        //door To The Moon
        if (PlayerPrefs.GetInt("DoorTTMOpen") == 1) doorToTheMoon.SetActive(false);
        else doorToTheMoon.SetActive(true);

        //door Going Under
        if (PlayerPrefs.GetInt("DoorGUOpen") == 1) doorGoingUnder.SetActive(false);
        else doorGoingUnder.SetActive(true);

        //door Ori
        if (PlayerPrefs.GetInt("DoorOOpen") == 1) doorOri.SetActive(false);
        else doorOri.SetActive(true);

        //door Before Your Eyes
        if (PlayerPrefs.GetInt("DoorBYEOpen") == 1) doorBeforeYourEyes.SetActive(false);
        else doorBeforeYourEyes.SetActive(true);

        //door My Friend Pedro
        if (PlayerPrefs.GetInt("DoorMFPOpen") == 1) doorMyFriendPedro.SetActive(false);
        else doorMyFriendPedro.SetActive(true);

        //door Cult Of The Lamb
        if (PlayerPrefs.GetInt("DoorCOTLOpen") == 1) doorCultOfTheLamb.SetActive(false);
        else doorCultOfTheLamb.SetActive(true);
    }
}
