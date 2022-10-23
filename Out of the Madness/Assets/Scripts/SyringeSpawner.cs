using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeSpawner : MonoBehaviour
{
    //[SerializeField] ScoreSystem scoreSystem;
    //[SerializeField] FullPowerSyringe fullPowerSyringe;

    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] GameObject syringe;


    [SerializeField] float minTimeBtwSpawns;
    [SerializeField] float maxTimeBtwSpawns;
    [SerializeField] bool canSpawn;

    private GameObject currentPoint;
    private int spawnPointIndex;

    void Start()
    {
        Invoke("SpawnSyringe", Random.Range(1f, 5f));
    }

    public void SpawnSyringe()
    {
        spawnPointIndex = Random.Range(0, spawnPoints.Length);
        currentPoint = spawnPoints[spawnPointIndex];

        float timeBtwSpawns = Random.Range(minTimeBtwSpawns, maxTimeBtwSpawns);

        if (GameObject.FindGameObjectWithTag("Syringe") == null)
        {
            if (FullPowerSyringe.instance.syringes != 5)
            {
                if (canSpawn)
                {
                    Instantiate(syringe, currentPoint.transform.position, Quaternion.identity);
                    Debug.Log("Syringe Spawned!");
                }
            }
        }

        Invoke("SpawnSyringe", timeBtwSpawns);
    }
}
