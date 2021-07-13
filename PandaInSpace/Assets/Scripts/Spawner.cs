using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float minTime;
    public float maxTime;

    public GameObject enemy;

    private float checkTime;   
    private float spawnTime;

    private Transform trans;

    void Start()
    {
        trans = GetComponent<Transform>();
        spawnTime = Random.Range(minTime, maxTime);
    }

    void Update()
    {
        checkTime += Time.deltaTime;
        if (checkTime >= spawnTime)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy() {
        Vector3 enemyPosition = new Vector3(Random.Range(14f, 16f), Random.Range(-5f, 5f));

        Instantiate(enemy, enemyPosition, Quaternion.identity);
        spawnTime = Random.Range(minTime, maxTime);
        checkTime = 0;
    }
}
