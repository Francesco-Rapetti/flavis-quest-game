using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorativeEye : MonoBehaviour
{
    [SerializeField] GameObject eye;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        eye.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        eye.SetActive(false);
    }
}
