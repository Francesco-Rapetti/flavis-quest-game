using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsabelPortal : MonoBehaviour
{
    [SerializeField] GameObject Isabel;

    public void IsabelDisappear()
    {
        Isabel.SetActive(false);
    }

    public void IsabelPortalDisappear()
    {
        gameObject.SetActive(false);
    }
}
