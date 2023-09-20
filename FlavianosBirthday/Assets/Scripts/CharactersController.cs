using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersController : MonoBehaviour
{
    [SerializeField] PlayerInfo playerInfo;

    [Header("Characters")]
    [SerializeField] GameObject chocobo;
    [SerializeField] GameObject isabel;
    [SerializeField] GameObject stray;
    [SerializeField] GameObject dog;
    [SerializeField] GameObject ku;


    [Header("Buttons")]
    [SerializeField] GameObject chocoboButton;
    [SerializeField] GameObject isabelButton;
    [SerializeField] GameObject strayButton;
    [SerializeField] GameObject dogButton;


    private void Update()
    {
        if (playerInfo.chocoboFound) chocoboButton.SetActive(false);
        if (playerInfo.isabelFound) isabelButton.SetActive(false);
        if (playerInfo.strayFound) strayButton.SetActive(false);
        if (playerInfo.dogFound) dogButton.SetActive(false);

        if (playerInfo.talkedToNaru1)
        {
            if (!(playerInfo.chocoboFound)) chocobo.SetActive(true);
            if (!(playerInfo.isabelFound)) isabel.SetActive(true);
            if (!(playerInfo.strayFound)) stray.SetActive(true);
            if (!(playerInfo.dogFound)) dog.SetActive(true);
            ku.SetActive(true);
        }
        

        

        /*if (playerInfo.talkedToNaru1 && !(playerInfo.chocoboFound && playerInfo.dogFound && playerInfo.isabelFound && playerInfo.strayFound))
        {
            chocobo.SetActive(true);
            isabel.SetActive(true);
            stray.SetActive(true);
            dog.SetActive(true);
            ku.SetActive(true);
        }
        else
        {
            chocobo.SetActive(false);
            isabel.SetActive(false);
            stray.SetActive(false);
            dog.SetActive(false);
            ku.SetActive(false);
        }*/
    }

   


}
