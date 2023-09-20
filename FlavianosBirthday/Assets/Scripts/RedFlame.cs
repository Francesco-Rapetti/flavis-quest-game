using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFlame : MonoBehaviour
{
    [SerializeField] GameObject sacrificedObject;

    public void ObjDisappear()
    {
        sacrificedObject.SetActive(false);
    }

    public void FlameDisappear()
    {
        gameObject.SetActive(false);
    }
}
