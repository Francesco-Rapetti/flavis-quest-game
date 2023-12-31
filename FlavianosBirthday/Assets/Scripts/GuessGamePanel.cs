using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuessGamePanel : MonoBehaviour
{
    [SerializeField] PlayerInfo playerInfo;
    [SerializeField] Text errorText;
    [SerializeField] string gameToGuess;
    [SerializeField] InputField inputField;
    [SerializeField] string character;

    [Header("Portals")]
    [SerializeField] GameObject chocoboPortal;
    [SerializeField] GameObject isabelPortal;
    [SerializeField] GameObject strayPortal;
    [SerializeField] GameObject dogPortal;

    string output;
    bool control = false;

    

    public void SayButton()
    {
        output = inputField.text;
        /*StringManager(output);
        StringManager(gameToGuess);*/
        Debug.Log(output);
        Debug.Log(StringManager(output));

        /* Isabel Doom Eternal option */
        if (character == "isabel" && StringManager(output) == "doom eternal")
        {
            control = true;
        }

        /* if player guess */
        if (StringManager(output) == StringManager(gameToGuess))
        {
            control = true;
        }

        if (control)
        {
            switch (character)
            {
                case "isabel": 
                    playerInfo.isabelFound = true; 
                    isabelPortal.SetActive(true);
                    break;
                case "chocobo": 
                    playerInfo.chocoboFound = true; 
                    chocoboPortal.SetActive(true);
                    break;
                case "dog": 
                    playerInfo.dogFound = true; 
                    dogPortal.SetActive(true);
                    break;
                case "stray": 
                    playerInfo.strayFound = true; 
                    strayPortal.SetActive(true);
                    break;
            }
            control = false;
            this.gameObject.SetActive(false);
        }
        else
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "WRONG";
        }
    }

    private string StringManager(string input)
    {
        string output = input;
        output = output.ToLower();
        while (output[output.Length - 1] == ' ')
        {
            output = output.Substring(0, output.Length - 1);
        }
        return output;
        
    }
}
