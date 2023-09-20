using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    float spawnTimer;
    [SerializeField] float spawnRate;
    [SerializeField] Bullet bullet;
    [SerializeField] PlayerInfo playerInfo;



    private void Update()
    {
        if (!playerInfo.bulletsStopped)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate)
            {
                spawnTimer -= spawnRate;
                Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
            }
        }
    }
}
