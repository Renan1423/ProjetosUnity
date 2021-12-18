using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimEnemy : MonoBehaviour
{
    private float initialFieldOfView;
    public float fieldOfView;

    public List<GameObject> enemiesList = new List<GameObject>();
    
    private GameObject closestEnemy;

    void Start()
    {
        initialFieldOfView = fieldOfView;
    }

    void Update()
    {
        LookingForEnemies();

        ClosestEnemy();
    }

    void LookingForEnemies() {

        bool enemyClose = Physics2D.OverlapCircle(transform.position, fieldOfView);

        Enemy[] enemiesInScene = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemiesInScene)
        {
            enemiesList.Add(enemy.gameObject);
        }
    }

    void ClosestEnemy()
    {
        foreach (GameObject enemyGameObject in enemiesList)
        {
            float dist = Vector2.Distance(enemyGameObject.transform.position, transform.position);
            if (dist < fieldOfView)
            {
                fieldOfView = dist;
                closestEnemy = enemyGameObject;
                Debug.Log("" + closestEnemy.name);
            }
        }

        if (closestEnemy == null) {
            fieldOfView = initialFieldOfView;
        }
    }

    bool EnemyInFieldOfView(float radius)
    {
        float TargetDistance = Vector2.Distance(closestEnemy.transform.position, transform.position);

        if (TargetDistance <= radius)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, fieldOfView);
    }
}
