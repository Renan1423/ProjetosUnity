using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Spawner : MonoBehaviour
{
    public float minTime;
    public float maxTime;

    public GameObject enemy;
    public GameObject enemy2;

    public int maxEnemies;
    public int currentEnemies;

    public bool isRight;

    private int enemyChoice;
    
    private float checkTime;
    private float spawnTime;

    private Transform trans;
    public Transform LeftSpawnPoint;
    public Transform RightSpawnPoint;

    private bool spawnActive;

    public CinemachineVirtualCamera CMVcam;
    public GameObject InvWall;
    public GameObject InvWall2;
    public GameObject InvWall3;
    public GameObject InvWall4;
    public GameObject GOPanel;
    public GameObject RightSpawner;

    void Start()
    {
        trans = GetComponent<Transform>();
        spawnTime = Random.Range(minTime, maxTime);

        spawnActive = false;
    }

    void Update()
    {
        checkTime += Time.deltaTime;
        if (checkTime >= spawnTime)
        {
            if (spawnActive == true && !isRight)
            {
                SpawnEnemy();
            }
            else if(isRight){
                SpawnEnemyRight();
            }
            
        }
    }

    void SpawnEnemy()
    {
        if (currentEnemies < maxEnemies)
        {
            Vector3 enemyPosition;
            enemyPosition = new Vector3(LeftSpawnPoint.position.x, Random.Range(-2.5f, 2.5f));
            enemyChoice = Random.Range(0, 2);
            if (enemyChoice == 0)
            {
                Instantiate(enemy, enemyPosition, Quaternion.identity);
            }
            else
            {
                Instantiate(enemy2, enemyPosition, Quaternion.identity);
            }
                spawnTime = Random.Range(minTime, maxTime);
                checkTime = 0;
                currentEnemies++;
        }
        else
        {
            StartCoroutine(EndSpawner());
        }
    }

    void SpawnEnemyRight()
    {
        if (currentEnemies < maxEnemies)
        {
            Vector3 enemyPosition;
            enemyPosition = new Vector3(RightSpawnPoint.position.x, Random.Range(-2.5f, 2.5f));
            enemyChoice = Random.Range(0, 2);
            if (enemyChoice == 0)
            {
                Instantiate(enemy, enemyPosition, Quaternion.identity);
            }
            else
            {
                Instantiate(enemy2, enemyPosition, Quaternion.identity);
            }
            spawnTime = Random.Range(minTime, maxTime);
            checkTime = 0;
            currentEnemies++;
        }
    }

    IEnumerator EndSpawner() {

        yield return new WaitForSeconds(1.5f);

        GOPanel.SetActive(true);
        GetComponent<BoxCollider2D>().enabled = false;
        var composer = CMVcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        composer.m_DeadZoneWidth = 0.25f;
        composer.m_DeadZoneHeight = 0.25f;
        InvWall.SetActive(false);
        InvWall2.SetActive(false);
        InvWall3.SetActive(false);
        InvWall4.SetActive(false);
        spawnActive = false;
        RightSpawner.SetActive(false);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            GetComponent<BoxCollider2D>().enabled = false;
            var composer = CMVcam.GetCinemachineComponent<CinemachineFramingTransposer>();
            composer.m_DeadZoneWidth = 2f;
            composer.m_DeadZoneHeight = 2f;
            InvWall.SetActive(true);
            InvWall2.SetActive(true);
            InvWall3.SetActive(true);
            InvWall4.SetActive(true);
            spawnActive = true;
            RightSpawner.SetActive(true);
        }    
    }

}
