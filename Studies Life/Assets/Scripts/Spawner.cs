using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float minTime;
    public float maxTime;

    public GameObject MathSymbol;

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
        Vector3 symbolPosition = new Vector3(Random.Range(-15f, 15f), Random.Range(8f, 10f));

        Instantiate(MathSymbol, symbolPosition, Quaternion.identity);
        spawnTime = Random.Range(minTime, maxTime);
        checkTime = 0;
    }
}
