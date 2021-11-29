using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // public variables
    public GameObject enemyRanged;
    public GameObject enemyMeele;
    public GameObject enemyRangedElite;
    public GameObject enemyMeeleElite;
    public int amount;

    // private variables
    private Vector3 spawnPoint;
    private float x;
    private float y;

    // Start is called before the first frame update
    void Start()
    {
        GameStats.gameStatsRef.countEnemies = amount;
        for (int i = 0; i < amount; i++)
        {
            spawnPoint = transform.position;
            x = Random.Range(-0.9f, 0.9f);
            y = Random.Range(-0.9f, 0.9f);
            spawnPoint.x += x;
            spawnPoint.y += y;
            spawnPoint.z = 0;
            int j = Random.Range(0, 25);
            if (j < 20 && j%2 == 0)
            {
                Instantiate(enemyRanged, spawnPoint, Quaternion.identity);
            }
            else if (j < 20 && j % 2 != 0)
            {
                Instantiate(enemyMeele, spawnPoint, Quaternion.identity);
            }
            else if (j < 22)
            {
                Instantiate(enemyRangedElite, spawnPoint, Quaternion.identity);
            }
            else
            {
                Instantiate(enemyMeeleElite, spawnPoint, Quaternion.identity);
            }
        }
    }
}
