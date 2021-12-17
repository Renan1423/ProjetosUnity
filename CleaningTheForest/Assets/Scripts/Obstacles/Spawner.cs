using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool Started;

    private SpeedController spdControl;
    private Player player;

    [Header("Spawn")]

    public float minTime = 1f;
    public float maxTime = 4f;

    private float initialMinTime;
    public float limitMinTime; // the minimum level of the minTime

    private float initialMaxTime;
    public float limitMaxTime; // the minimum level of the maxTime

    public float checkTime;
    public float spawnTime;

    public GameObject[] Obstacles;


    void Awake()
    {
        spdControl = GetComponent<SpeedController>();
        player = FindObjectOfType<Player>();

        initialMinTime = minTime;
        initialMaxTime = maxTime;

        spawnTime = Random.Range(minTime, maxTime);
    }


    void Update()
    {
        if (Started)
        {
            checkTime += Time.deltaTime;
            if (checkTime >= spawnTime)
            {
                SpawnEnemy();
            }
        }

        if (minTime > limitMinTime)
        {
            minTime = initialMinTime - (0.05f * (player.GetComponent<Player>().Points / 50));
        }
        else {
            minTime = limitMinTime;
        }

        if (maxTime < limitMaxTime)
        {
            maxTime = initialMaxTime - (0.05f * (player.GetComponent<Player>().Points / 50));
        }
        else {
            maxTime = limitMaxTime;
        }




    }

    void SpawnEnemy() {
        GameObject obstacle = Instantiate(Obstacles[Random.Range(0, Obstacles.Length)], transform.position, Quaternion.identity) as GameObject;
        obstacle.GetComponent<Trash>().Speed = spdControl.objSpeed;
        spawnTime = Random.Range(minTime, maxTime);
        checkTime = 0;
    }
}
