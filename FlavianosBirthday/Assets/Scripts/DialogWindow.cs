using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogWindow : MonoBehaviour
{
    [SerializeField]
    PlayerInfo playerInfo;

    [SerializeField]
    Dialog[] dialogs;

    [SerializeField]
    DialogManager dialogManager;


    private void Start()
    {
        
    }

    public void Interact()
    {
        StartCoroutine(dialogManager.ShowDialog(dialogs));
    }
}
