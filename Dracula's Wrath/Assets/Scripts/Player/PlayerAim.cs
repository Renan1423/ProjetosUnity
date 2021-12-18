using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public float speed;

    private EnemyCount enemyCount;

    void Start()
    {
        Cursor.visible = false;

        enemyCount = FindObjectOfType<EnemyCount>();
    }
    void Update()
    {

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        //float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + rotationOffset));

        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 0;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (enemyCount.numberOfEnemies <= 0 || Player.isPaused)
        {
            Cursor.visible = true;
        }
        else {
            Cursor.visible = false;
        }
        
   }
}
