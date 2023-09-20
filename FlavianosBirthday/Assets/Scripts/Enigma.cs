using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enigma : MonoBehaviour
{
    [SerializeField]
    InputField inputField;

    [SerializeField] 
    PlayerInfo playerInfo;

    [SerializeField]
    GameObject findingParadiseCover;

    [SerializeField]
    DialogManager dialogStaticBoyManager;

    [SerializeField]
    Text error;

    [SerializeField]
    DialogWindow dialogStatickBoyWindow;

    [SerializeField] GameObject objKey;


    private string tool = "finding paradise";

    public void SendButton()
    {
        string output = inputField.text;
        Debug.Log(output);
        Debug.Log(StringManager(output));
        if (StringManager(output) == StringManager(tool))
        {
            Debug.Log("bravo!");
            playerInfo.enigmaSolved = true;
            findingParadiseCover.SetActive(true);
            objKey.SetActive(true);
            dialogStaticBoyManager.currentDialog = 1;
            gameObject.SetActive(false);
            dialogStatickBoyWindow.Interact();
        }
        else
        {
            Debug.Log("Sbagliato!");
            error.gameObject.SetActive(true);
            error.text = $"ERROR: \"{inputField.text}\" NOT FOUND";
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
