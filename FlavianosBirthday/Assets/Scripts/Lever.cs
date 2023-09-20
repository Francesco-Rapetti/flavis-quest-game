using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Lever : MonoBehaviour
{
    [SerializeField] PlayerInfo playerInfo;
    [SerializeField] Sprite leverOn;
    [SerializeField] Sprite leverOff;
    [SerializeField] GameObject leverButton;
    bool active = true;

    public void ToggleLever()
    {
        if (active)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = leverOff;
            active = false;
            playerInfo.bulletsStopped = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = leverOn;
            active = true;
            playerInfo.bulletsStopped = false;
        }
    }

    private void Update()
    {
        if (playerInfo.leverPulled)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = leverOff;
            active = false;
            playerInfo.bulletsStopped = true;
            leverButton.SetActive(false);
        }
    }
}
