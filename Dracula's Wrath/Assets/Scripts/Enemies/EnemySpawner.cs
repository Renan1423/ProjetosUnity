using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] Enemies;

    public float SpawnRadius;

    private int numberOfEnemies;

    public int minNumberOfEnemies;
    public int maxNumberOfEnemies;

    private bool isActive = false;
    private bool hasSpawned;

    private EnemyCount enemyCount;

    void Start()
    {
        enemyCount = FindObjectOfType<EnemyCount>();

        //Add the number of enemies to the enemies count in the floor
        numberOfEnemies = Random.Range(minNumberOfEnemies, maxNumberOfEnemies);

        enemyCount.numberOfEnemies += numberOfEnemies + 1;
    }


    void Update()
    {
        if (isActive && !hasSpawned) {
            for (int i = 0; i <= numberOfEnemies; i++) {
                int enemyType = Random.Range(0,Enemies.Length);

                Vector3 position = new Vector3(transform.position.x + Random.Range(-SpawnRadius/2, SpawnRadius/2), transform.position.y + Random.Range(-SpawnRadius / 2, SpawnRadius / 2), transform.position.z);

                Instantiate(Enemies[enemyType], position, Quaternion.identity);

                if (i == numberOfEnemies) {
                    hasSpawned = true;
                }
            }
        }

        if (hasSpawned) {
            Destroy(gameObject, 1f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isActive) {
            isActive = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, SpawnRadius);
    }
}
