using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField]
    GameObject dialogBox;

    [SerializeField]
    Text dialogText;

    [SerializeField]
    int lettersPerSecond;

    [SerializeField]
    PlayerInfo playerInfo;

    [SerializeField]
    public int currentDialog = 0;

    Dialog[] dialogs;
    public int currentLine = 0;
    bool isTyping;

    [HideInInspector]
    public bool dialogBoxActive = false;

    

    private void Awake()
    {
        dialogBox.SetActive(false);
    }

    public IEnumerator ShowDialog(Dialog[] dialogs)
    {
        yield return new WaitForEndOfFrame();
        dialogBoxActive = true;
        playerInfo.isTalking = true;
        this.dialogs = dialogs;
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialogs[currentDialog].Lines[0]));
    }

    public void HandleUpdate()
    {
        if (Input.GetMouseButton(0) && !isTyping)
        {
            //Debug.Log(currentDialog);
            ++currentLine;
            if (currentLine < dialogs[currentDialog].Lines.Count)
            {
                StartCoroutine(TypeDialog(dialogs[currentDialog].Lines[currentLine]));
            }
            else
            {
                currentLine = 0;
                dialogBoxActive = false;
                dialogBox.SetActive(false);
                playerInfo.isTalking = false;
            }
        }
    }


    public IEnumerator TypeDialog(string line)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false;
    }
}
