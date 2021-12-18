using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform player;

    /*private void Update()
    {
        if (player.GetComponent<Player>().direction.sqrMagnitude > 0)
        {
            transform.eulerAngles = new Vector3(0, 0,180);
        }
        if(player.GetComponent<Player>().direction.sqrMagnitude < 0) {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        
    }*/

    void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
}
