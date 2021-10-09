using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public int amount;
    private Vector3 spawnPoint;
    private float x;
    private float y;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < amount; i++)
        {
            spawnPoint = transform.position;
            x = Random.Range(-0.9f, 0.9f);
            y = Random.Range(-0.9f, 0.9f);
            spawnPoint.x += x;
            spawnPoint.y += y;
            Instantiate(enemy, spawnPoint, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
