using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocoboPortal : MonoBehaviour
{
    [SerializeField] GameObject chocobo;

    public void ChocoboDisappear()
    {
        chocobo.SetActive(false);
    }

    public void ChocoboPortalDisappear()
    {
        gameObject.SetActive(false);
    }
}
