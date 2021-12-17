using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public bool Started;

    private SpeedController spdControl;

    [Header("Spawn")]

    public float minTime = 1f;
    public float maxTime = 4f;

    public float checkTime;
    public float spawnTime;

    public GameObject[] spawnablePowerUps;


    void Start()
    {
        spdControl = GetComponent<SpeedController>();

        spawnTime = Random.Range(minTime, maxTime);
    }

    void Update()
    {
        if (Started)
        {
            checkTime += Time.deltaTime;
            if (checkTime >= spawnTime)
            {
                SpawnPowerUp();
            }
        }

    }

    void SpawnPowerUp()
    {
        GameObject obstacle = Instantiate(spawnablePowerUps[Random.Range(0, spawnablePowerUps.Length)], transform.position, Quaternion.identity) as GameObject;
        obstacle.GetComponent<PowerUp>().Speed = spdControl.objSpeed;
        spawnTime = Random.Range(minTime, maxTime);
        checkTime = 0;
    }
}
