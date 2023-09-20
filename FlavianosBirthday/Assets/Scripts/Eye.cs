using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    [SerializeField] PlayerBeforeYourEyes player;
    [SerializeField] GameObject eye;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            eye.SetActive(true);
        }
    }

    private void Update()
    {
        if (player.transform.position.x == player.startPos.x && player.transform.position.y == player.startPos.y)
        {
            eye.SetActive(false);
        }
    }
}
