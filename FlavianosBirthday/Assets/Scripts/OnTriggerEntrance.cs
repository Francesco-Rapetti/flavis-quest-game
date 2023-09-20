using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTriggerEntrance : MonoBehaviour
{
    [SerializeField]
    string levelName;

    [SerializeField]
    PlayerInfo playerInfo;

    [SerializeField]
    GameObject player;

    [SerializeField]
    LevelLoader levelLoader;

    [Header("Position offset")]
    [SerializeField]
    float xOffset;
    [SerializeField]
    float yOffset;

    public void loadLevel()
    {
        SceneManager.LoadScene(levelName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInfo.playerPosition.x = player.transform.position.x + xOffset;
            playerInfo.playerPosition.y = player.transform.position.y + yOffset;
            playerInfo.playerPosition.z = player.transform.position.z;
            playerInfo.currentScene = levelName;
            levelLoader.LoadLevel(levelName);
        }
    }
}
