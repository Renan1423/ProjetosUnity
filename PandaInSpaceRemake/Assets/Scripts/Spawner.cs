using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemies; //List of enemies that will spawn
    public float spawnDelay;
    private bool hasStarted = false;
    private bool hasSpawned = false;

    public Transform spawnPoint; // Position where the enemy will spawn

    void Update()
    {
        if (hasSpawned) {
            Destroy(gameObject);
        }
    }

    IEnumerator SpawnEnemies() {

        yield return new WaitForSeconds(0.001f);

        if (!hasSpawned)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                Instantiate(enemies[i], spawnPoint.position, Quaternion.identity);

                yield return new WaitForSeconds(spawnDelay);
            }
            hasSpawned = true;
        }
        else {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !hasStarted) {
            hasStarted = true;
            StartCoroutine(SpawnEnemies());
        }
    }
}
