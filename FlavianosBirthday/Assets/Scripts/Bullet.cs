using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] PlayerInfo playerInfo;
    public float speed;
    public bool goRight;

    Vector2 startPos;

    private void Awake()
    {
        if (!goRight)
        {
            GetComponentInChildren<Bullet>().gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            GetComponentInChildren<Bullet>().gameObject.transform.rotation = Quaternion.Euler(0, 0, -90);
        }

        if (!goRight)
        {
            speed *= -1;
        }

        startPos = transform.position;
    }

    private void Update()
    {
        
        
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        
        if (goRight)
        {
            if (transform.position.x > startPos.x + 18)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (transform.position.x < startPos.x - 18) 
            {
                Destroy(gameObject);
            }
        }
    }
}
