using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtEnemy : MonoBehaviour
{
    public GameObject enemy;

    public float fieldOfView;

    public float lookSpeed = 200;

    private Quaternion targetRotation;
    private Quaternion lookAt;

    // Update is called once per frame
    void Update()
    {
        if (EnemyInFieldOfView(fieldOfView))
        {
            Vector2 direction = enemy.transform.position - transform.position;
            targetRotation = Quaternion.LookRotation(direction);
            lookAt = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * lookSpeed);
            transform.rotation = lookAt;
        }
        else
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, Time.deltaTime * lookSpeed);
        }
    }

    bool EnemyInFieldOfView(float radius)
    {

        float TargetDistance = Vector2.Distance(enemy.transform.position, transform.position);

        if (TargetDistance <= radius)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    

}
