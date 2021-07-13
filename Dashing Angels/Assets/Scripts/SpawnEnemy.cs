using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    public float minTime;
    public float maxTime;
    float spawnTime;

    public GameObject enemyPrefab;

    float timeCount;

    void Start()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }

    void Update()
    {
        timeCount += Time.deltaTime;

        if (timeCount > spawnTime)
        {

            Vector3 enemyPos = new Vector3(transform.position.x, transform.position.y, 0f);
            Instantiate(enemyPrefab, enemyPos, transform.rotation);

            spawnTime = Random.Range(minTime, maxTime);

            timeCount = 0f;

        }
    }
}
