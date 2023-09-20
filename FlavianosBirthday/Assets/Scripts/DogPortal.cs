using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPortal : MonoBehaviour
{
    [SerializeField] GameObject dog;

    public void DogDisappear()
    {
        dog.SetActive(false);
    }

    public void DogPortalDisappear()
    {
        gameObject.SetActive(false);
    }
}
