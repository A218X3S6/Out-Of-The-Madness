using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnConfig : MonoBehaviour
{
    public static float minTimeBtwSpawns = 2f;
    public static float maxTimeBtwSpawns = 4f;
    
    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] GameObject[] enemies;
    [SerializeField] bool canSpawn;

    private GameObject currentPoint;
    private int spawnPointIndex;

    void Start()
    {     
        Invoke("SpawnEnemy", Random.Range(0.5f, 2f));
    }

    void SpawnEnemy()
    {
        currentPoint = spawnPoints[spawnPointIndex];
        float timeBtwSpawns = Random.Range(minTimeBtwSpawns, maxTimeBtwSpawns);

        if (canSpawn)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], currentPoint.transform.position, Quaternion.identity);
        }

        Debug.Log("Max spawn time " + maxTimeBtwSpawns);

        Invoke("SpawnEnemy", timeBtwSpawns);
    }

}

