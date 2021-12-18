using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimManager : MonoBehaviour
{

    LookAtEnemy lookAtEnemy;
    List<GameObject> enemiesList = new List<GameObject>();
    private GameObject closestEnemy;
    public float maxRange = 100;

    void Start()
    {
        lookAtEnemy = GetComponent<LookAtEnemy>();
        Enemy[] enemiesInScene = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemiesInScene)
        {
            enemiesList.Add(enemy.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ClosestEnemy()
    {
        float range = maxRange;
        foreach (GameObject enemyGameObject in enemiesList)
        {
            float dist = Vector2.Distance(enemyGameObject.transform.position, transform.position);
            if (dist < range)
            {
                range = dist;
                closestEnemy = enemyGameObject;
                Debug.Log("" + closestEnemy.name);
            }
        }

        lookAtEnemy.enemy = closestEnemy;
    }
}
