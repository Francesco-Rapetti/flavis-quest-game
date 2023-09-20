using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrayPortal : MonoBehaviour
{
    [SerializeField] GameObject stray;

    public void StrayDisappear()
    {
        stray.SetActive(false);
    }

    public void StrayPortalDisappear()
    {
        gameObject.SetActive(false);
    }
}
