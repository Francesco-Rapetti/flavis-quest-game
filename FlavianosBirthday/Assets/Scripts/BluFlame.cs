using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluFlame : MonoBehaviour
{
    [SerializeField] GameObject cultOfTheLambCover;

    public void GameAppear()
    {
        cultOfTheLambCover.SetActive(true);
    }

    public void FlameDisappear()
    {
        gameObject.SetActive(false);
    }
}
